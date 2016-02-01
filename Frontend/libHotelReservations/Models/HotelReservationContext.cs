namespace libHotelReservations.Models
{
    using System;
    using System.Data.Entity;

    public class HotelReservationContext : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « HotelReservationContext » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « libHotelReservations.HotelReservationContext » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « HotelReservationContext » dans le fichier de configuration de l'application.
        public HotelReservationContext()
            : base("name=HotelReservationContext")
        {
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<HotelReservation> HotelReservations { get; set; }
    }

    public class HotelReservation
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}