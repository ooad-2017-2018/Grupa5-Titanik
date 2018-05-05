﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelBookApp.Model
{
   public abstract class Korisnik : IPrint
    {
        static int id = 0;
        private String imeKorisnika;
        private String prezimeKorisnika;
        private String jmbgKorisnika;
        private DateTime datumRodjenjaKorisnika;
        private String kontaktTelefon;
        private String emailAdresa;


        public Korisnik(String ime, String prezime, String jmbg, DateTime datumRodjenja, String brojTelefona, String email)
        {
            id++;
            imeKorisnika = ime;
            prezimeKorisnika = prezime;
            jmbgKorisnika = jmbg;
            datumRodjenjaKorisnika = datumRodjenja;
            kontaktTelefon = brojTelefona;
            emailAdresa = email;
        }

        public string ImeKorisnika { get => imeKorisnika; set => imeKorisnika = value; }
        public string PrezimeKorisnika { get => prezimeKorisnika; set => prezimeKorisnika = value; }
        public string JmbgKorisnika { get => jmbgKorisnika; set => jmbgKorisnika = value; }
        public DateTime DatumRodjenja { get => datumRodjenjaKorisnika; set => datumRodjenjaKorisnika = value; }
        public string KontaktTelefon { get => kontaktTelefon; set => kontaktTelefon = value; }
        public string EmailAdresa { get => emailAdresa; set => emailAdresa = value; }

       
        public virtual string IspisiString()
        {
            return string.Format("Ime: {0}" + Environment.NewLine +
                                 "Prezime: {1}" + Environment.NewLine +
                                 "Maticni broj: {2}" + Environment.NewLine +
                                 "Datum rođenja: {3}", imeKorisnika, prezimeKorisnika, jmbgKorisnika, datumRodjenjaKorisnika);
        }
    
    }
}
