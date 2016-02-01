using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace libAirports.Models
{
    
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>()
                        .HasRequired(a => a.DepartureAirport)
                        .WithMany()
                        .HasForeignKey(u => u.DepartureAirportId);

            modelBuilder.Entity<Flight>()
                       .HasRequired(a => a.ArrivalAirport)
                       .WithMany()
                       .HasForeignKey(u => u.ArrivalAirportId).WillCascadeOnDelete(false);
        }
        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
    }

    public class Airport
    {
        [Key]
        public int Id { get; set; }
        [Index("CodeIndex", IsUnique = true), StringLength(3), Required]
        public String Code { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String City { get; set; }
        [Required]
        public String Country { get; set; }
        public String Timezone { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
    public class Flight
    {
        public int Id { get; set; }
        [Required]
        public int DepartureAirportId { get; set; }
        [Required]
        public int ArrivalAirportId { get; set; }

        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
    }
}