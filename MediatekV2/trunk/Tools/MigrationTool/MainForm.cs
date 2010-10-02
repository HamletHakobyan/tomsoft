using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Mediatek.Data.EntityFramework;
using System.Data.SQLite;
using Mediatek.Entities;
using System.IO;

namespace MigrationTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            try
            {
                dlgSource.InitialDirectory = Path.GetFullPath(txtSource.Text);
                dlgSource.FileName = Path.GetFileName(txtSource.Text);
            }
            catch { }
            if (dlgSource.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = dlgSource.FileName;
            }
        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            try
            {
                dlgDestination.InitialDirectory = Path.GetFullPath(txtDestination.Text);
                dlgDestination.FileName = Path.GetFileName(txtDestination.Text);
            }
            catch { }
            if (dlgDestination.ShowDialog() == DialogResult.OK)
            {
                txtDestination.Text = dlgDestination.FileName;
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            prgOverall.Value = 0;
            this.Enabled = false;
            bgwMigration.RunWorkerAsync();
        }

        private static readonly Guid _directorRoleGuid = Guid.Parse("a3cb6aad-3149-4606-902a-f04317249a1d");

        private void bgwMigration_DoWork(object sender, DoWorkEventArgs e)
        {
            string sourceConnectionString = string.Format("Data source={0}", txtSource.Text);
            string destConnectionString = string.Format("Data source={0}", txtDestination.Text);

            int stepCount = 6;
            int iStep = 0;

            using (var sourceConnection = new SQLiteConnection(sourceConnectionString))
            using (var destContext = MediatekContext.GetContext("System.Data.SqlServerCe.3.5", destConnectionString))
            {
                // Load old database
                var ds = new MoviesDataSet();
                FillTable(sourceConnection, ds.language);
                FillTable(sourceConnection, ds.country);
                FillTable(sourceConnection, ds.director);
                FillTable(sourceConnection, ds.movie);
                FillTable(sourceConnection, ds.media_type);
                FillTable(sourceConnection, ds.lend);

                destContext.Connection.Open();
                using (var transaction = destContext.Connection.BeginTransaction())
                {
                    // Images
                    var tablesWithPicture = new[]
                        {
                            new { TableName = "movie", ImageField = "cover", ImageIdField = "pictureid", NameField = "title" },
                            new { TableName = "director", ImageField = "picture", ImageIdField = "pictureid", NameField = "name" },
                            new { TableName = "language", ImageField = "symbol", ImageIdField = "flagid", NameField = "name" },
                            new { TableName = "country", ImageField = "flag", ImageIdField = "flagid", NameField = "name" }
                        };

                    foreach (var tbl in tablesWithPicture)
                    {
                        var table = ds.Tables[tbl.TableName];
                        foreach (DataRow row in table.Rows)
                        {
                            Guid imageId = Guid.NewGuid();
                            row[tbl.ImageIdField] = imageId;

                            if (row.IsNull(tbl.ImageField))
                                continue;
                            
                            var image = new Mediatek.Entities.Image
                            {
                                Id = imageId,
                                Name = row[tbl.NameField] as string
                            };
                            var imageData = new Mediatek.Entities.ImageData
                            {
                                Image = image,
                                Bytes = row[tbl.ImageField] as byte[]
                            };
                            destContext.AddImage(image);
                            destContext.AddImageData(imageData);
                        }
                        destContext.SaveChanges();
                    }

                    iStep++;
                    bgwMigration.ReportProgress(100 * iStep / stepCount);

                    // Language
                    foreach (var row in ds.language)
                    {
                        row.guid = Guid.NewGuid();
                        var language = new Language
                        {
                            Id = row.guid,
                            Name = row.name,
                            Code = row.code,
                            Flag = destContext.Images.FirstOrDefault(img => img.Id == row.flagid)
                        };
                        destContext.AddLanguage(language);
                    }
                    destContext.SaveChanges();

                    iStep++;
                    bgwMigration.ReportProgress(100 * iStep / stepCount);

                    // Countries
                    foreach (var row in ds.country)
                    {
                        row.guid = Guid.NewGuid();
                        var country = new Country
                        {
                            Id = row.guid,
                            Name = row.name,
                            Flag = destContext.Images.FirstOrDefault(img => img.Id == row.flagid),
                            Languages = destContext.Languages
                                        .AsEnumerable()
                                        .Where(l => l.Id == (row.Islanguage_idNull()
                                                            ? Guid.Empty
                                                            : row.languageRow.guid))
                                        .ToList()
                        };
                        destContext.AddCountry(country);
                    }
                    destContext.SaveChanges();

                    iStep++;
                    bgwMigration.ReportProgress(100 * iStep / stepCount);

                    // Persons
                    foreach (var row in ds.director)
                    {
                        row.guid = Guid.NewGuid();
                        var director = new Person
                        {
                            Id = row.guid,
                            DisplayName = row.name,
                            Picture = destContext.Images.FirstOrDefault(img => img.Id == row.pictureid),
                            Countries = destContext.Countries
                                        .AsEnumerable()
                                        .Where(c => c.Id == (row.Iscountry_idNull()
                                                            ? Guid.Empty
                                                            : row.countryRow.guid))
                                        .ToList()
                        };
                        destContext.AddPerson(director);
                    }
                    destContext.SaveChanges();

                    var borrowerGuids = ds.lend.AsEnumerable()
                                        .Select(r => r.lent_to)
                                        .Distinct()
                                        .ToDictionary(s => s, s => Guid.NewGuid());

                    foreach (var row in ds.lend)
                    {
                        row.borrower_guid = borrowerGuids[row.lent_to];
                    }

                    foreach (var kvp in borrowerGuids)
                    {
                        var borrower = new Person
                        {
                            Id = kvp.Value,
                            DisplayName = kvp.Key
                        };
                        destContext.AddPerson(borrower);
                    }
                    destContext.SaveChanges();

                    iStep++;
                    bgwMigration.ReportProgress(100 * iStep / stepCount);

                    // Medias
                    foreach (var row in ds.movie)
                    {
                        row.guid = Guid.NewGuid();
                        var movie = new Movie
                        {
                            Id = row.guid,
                            Title = row.title,
                            OriginalTitle = row.original_title,
                            LanguageId = row.languageRow.guid,
                            Year = row.IsyearNull() ? (int?)null : int.Parse(row.year),
                            Picture = destContext.Images.FirstOrDefault(img => img.Id == row.pictureid)
                        };
                        destContext.AddMedia(movie);

                        if (!row.Isdirector_idNull())
                        {
                            var contrib = new Contribution
                            {
                                MediaId = row.guid,
                                PersonId = row.directorRow.guid,
                                RoleId = _directorRoleGuid
                            };

                            if (!row.directorRow.Iscountry_idNull())
                                movie.Countries = destContext.Countries.Where(c => c.Id == row.directorRow.countryRow.guid).ToList();

                            destContext.AddContribution(contrib);
                        }
                    }
                    destContext.SaveChanges();

                    iStep++;
                    bgwMigration.ReportProgress(100 * iStep / stepCount);

                    // Loans
                    foreach (var row in ds.lend)
                    {
                        row.guid = Guid.NewGuid();
                        var loan = new Loan
                        {
                            Id = row.guid,
                            PersonId = row.borrower_guid,
                            LoanDate = row.lent_date,
                            ReturnDate = row.Isreturn_dateNull() ? (DateTime?)null : row.return_date,
                            MediaId = row.movieRow.guid
                        };
                        destContext.AddLoan(loan);
                    }
                    destContext.SaveChanges();

                    iStep++;
                    bgwMigration.ReportProgress(100 * iStep / stepCount);

                    transaction.Commit();
                    bgwMigration.ReportProgress(100);
                }
            }
        }

        private int FillTable(SQLiteConnection connection, DataTable table)
        {
            using (var adapter = new SQLiteDataAdapter(string.Format("SELECT * FROM {0} ORDER BY id", table.TableName), connection))
            {
                return adapter.Fill(table);
            }
        }

        private void bgwMigration_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgOverall.Value = e.ProgressPercentage;
        }

        private void bgwMigration_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Error : " + e.Error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Migration complete");
            }
            this.Enabled = true;
        }
    }
}
