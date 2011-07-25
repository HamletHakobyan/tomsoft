using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Mediatek.Helpers;

namespace Mediatek.Design
{
    public class DesignMovieViewModel : DesignMediaViewModel
    {
        public DesignMovieViewModel()
        {
            Title = "Eh mec ! Elle est où ma caisse ?";
            OriginalTitle = "Dude, Where's My Car?";
            Year = 2000;
            Rating = 4;
            Duration = 83;
            Description =
                @"Jesse et Chester se réveillent après avoir passé une nuit à faire la fête et perdu leur voiture qui contenait le cadeau d'anniversaire de leurs petites amies, les jumelles Wanda et Wilma. En tentant de retracer leur dernière nuit, ils découvrent qu'ils ont en leur possession le disrupteur dimensionnel, un ""objet énigmatique très puissant, et l'énigme qui l'entoure n'a d'égale que son immense puissance."" Le disrupteur dimensionnel se révèle être l'objet de convoitise d'une secte menée par un dénommé Zoltan.";
            Comment = "Du gros n'importe quoi";
            DateAdded = DateTime.Today;

            Picture = new BitmapImage(PackUri.MakePackUri("Images/moviedefault.jpg"));
            DirectorNames = new[] { "Danny Leiner" };
            Language = new { Name = "Anglais" };
            Countries = new[] { new { Name = "États-Unis" } };
            Loans = new[] { new { Person = new { DisplayName = "Joe" }, LoanDate = DateTime.Today} };

            Contributions = new[]
                                {
                                    new { RoleName = "Director", ContributorName = "Danny Leiner" },
                                    new { RoleName = "Producer", ContributorName = "Broderick Johnson" },
                                    new { RoleName = "Producer", ContributorName = "Andrew A. Kosove" },
                                    new { RoleName = "Producer", ContributorName = "Gil Netter" },
                                    new { RoleName = "Producer", ContributorName = "Wayne Allan Rice" },
                                    new { RoleName = "Co-producer", ContributorName = "Nancy Paloian" },
                                    new { RoleName = "Writer", ContributorName = "Philip Stark" },
                                };
            var contributionView = CollectionViewSource.GetDefaultView(Contributions);
            contributionView.GroupDescriptions.Add(new PropertyGroupDescription("RoleName"));
        }

        public int? Duration { get; set; }
        public string[] DirectorNames { get; set; }
    }
}
