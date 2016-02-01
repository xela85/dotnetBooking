namespace libHotels.Models
{
    using System.Data.Entity;

    public class HotelContext : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « HotelContext » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « libHotels.Models.HotelContext » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « HotelContext » dans le fichier de configuration de l'application.
        public HotelContext()
            : base("name=HotelContext")
        {
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Hotel> Hotels { get; set; }
    }

    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}