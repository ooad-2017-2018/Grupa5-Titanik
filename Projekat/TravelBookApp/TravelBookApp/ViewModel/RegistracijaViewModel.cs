﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace TravelBookApp
{
    public class RegistracijaViewModel
    {
        public MessageDialog Poruka { get; set; }


        //registracija
        public String NazivAgencije { get; set; }
        public Kartica PodaciOBankovnomRacunu { get; set; }
        public String KontaktTelefon { get; set; }
        public String EmailAdresa { get; set; }
        public String Lokacija { get; set; }


        public RegistracijaViewModel()
        {
            NazivAgencije = "";
            PodaciOBankovnomRacunu = new Kartica();
            KontaktTelefon = "";
            EmailAdresa = "";
            Lokacija = "";
        }

        public bool validirajEmail()
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(EmailAdresa);
            if (match.Success)
            {
                return true;
            }

            return false;
        }

        public bool jednakiRacuni(Kartica neka, Kartica trenutna)  // jel samo po brojevima gledam?
        {
            return neka.Broj == trenutna.Broj;
        }


        public void registrujAgneciju(String naziv, Kartica kartica, String telefon, String email,String grad, String adresa, String sifra)
        {
            Agencija nova = new Agencija(naziv, kartica, telefon, email, grad, adresa,sifra);
          /*  if (!validirajEmail())
            {
                Poruka = new MessageDialog("Neispravna mail adresa.");
                Poruka.ShowAsync();
               // return;
            }*/
            if (Globalna.nasaAgencija.Agencije.Any(agencije => agencije.NazivAgencije.Equals(nova.NazivAgencije) || agencije.KontaktTelefon.Equals(nova.KontaktTelefon) || jednakiRacuni(agencije.PodaciOBankovnomRacunu, nova.PodaciOBankovnomRacunu) || agencije.EmailAdresa.Equals(nova.EmailAdresa)))
            {
                Poruka = new MessageDialog("Neki od navedenih podataka već postoje.");
                Poruka.ShowAsync();

            }
            else
            {
                Globalna.nasaAgencija.Agencije.Add(nova);
            }
        }

    }
}
