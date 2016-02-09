using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using data.messaging;
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

}