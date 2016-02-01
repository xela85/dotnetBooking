namespace libAirports.Models
{
    using System.Data.Entity;

    public class AirportContext : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « AirportContext » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « libAirports.Models.AirportContext » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « AirportContext » dans le fichier de configuration de l'application.
        public AirportContext()
            : base("name=AirportContext")
        {
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Airport> MyAirports { get; set; }
    }

    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}