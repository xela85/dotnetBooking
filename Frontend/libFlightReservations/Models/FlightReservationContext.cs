namespace libFlightReservations.Models
{
    using System.Data.Entity;
    using data.messaging;

    public class FlightReservationContext : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « FlightReservationContext » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « libFlightReservations.Models.FlightReservationContext » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « FlightReservationContext » dans le fichier de configuration de l'application.
        public FlightReservationContext()
            : base("name=FlightReservationContext")
        {
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<FlightReservation> FlightReservations { get; set; }
    }


}