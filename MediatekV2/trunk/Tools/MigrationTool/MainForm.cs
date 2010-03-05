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

            int stepCount = 5;
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
                    // Language
                    foreach (var row in ds.language)
                    {
                        row.guid = Guid.NewGuid();
                        var language = new Language
                        {
                            Id = row.guid,
                            Name = row.name,
                            Code = row.code,
                            Flag = row.symbol
                        };
                        destContext.Languages.AddObject(language);
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
                            Flag = row.flag,
                            Languages = destContext.Languages
                                        .AsEnumerable()
                                        .Where(l => l.Id == (row.Islanguage_idNull()
                                                            ? Guid.Empty
                                                            : row.languageRow.guid))
                                        .ToList()
                        };
                        destContext.Countries.AddObject(country);
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
                            Picture = row.picture,
                            Countries = destContext.Countries
                                        .AsEnumerable()
                                        .Where(c => c.Id == (row.Iscountry_idNull()
                                                            ? Guid.Empty
                                                            : row.countryRow.guid))
                                        .ToList()
                        };
                        destContext.Persons.AddObject(director);
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
                        destContext.Persons.AddObject(borrower);
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
                            Picture = row.cover
                        };
                        destContext.Medias.AddObject(movie);

                        if (!row.Isdirector_idNull())
                        {
                            var contrib = new Contribution
                            {
                                MediaId = row.guid,
                                PersonId = row.directorRow.guid,
                                RoleId = _directorRoleGuid
                            };
                            destContext.Contributions.AddObject(contrib);
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
                        destContext.Loans.AddObject(loan);
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
