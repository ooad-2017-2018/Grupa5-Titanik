﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TravelBookApp.Model;
using TravelBookApp.ViewModel;
using Windows.UI.Popups;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelBookApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KreiranjePutovanja : Page
    {
        static KreiranjePutovanjaViewModel putovanjeVM = new KreiranjePutovanjaViewModel();
        static List<String> naziviDestinacija = new List<string>();
        static List<String> naziviHotela = new List<string>();
        List<String> autobusi = new List<string>();
        List<String> avioKompanije = new List<string>();
        List<String> listaPolaznihLetova = new List<string>();
        List<String> listaPovratnihLetova = new List<string>();
      //  String odabranaDestinacija;
        String odabraniHotel;
        Boolean istaknuto;
      
        //STAVITI DA SU CRNI border-i NEVIDLJIVI PA DA SE AKTIVIRAJU KAD SKONTAM GDJE TREBA IĆI TO

        public KreiranjePutovanja()
        {
            this.InitializeComponent();
            gLet.Visibility = Visibility.Collapsed;
            putovanjeVM.dodajListuBusPrevoza();

            //HOTELI
           

          
            //DESTINACIJE
            Destinacija prva = new Destinacija("Sarajevo", "Bosna i Hercegovina", Kontinenti.Evropa);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            putovanjeVM.dodajNoviHotel("Radon Plaza", 600, 30, prva, 150);

            prva = new Destinacija("Zagreb", "Hrvatska", Kontinenti.Evropa);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Tokio", "Japan", Kontinenti.Azija);

            Globalna.nasaAgencija.Destinacije.Add(prva);
            putovanjeVM.dodajNoviHotel("Hokaido Hotel", 500, 30, prva, 250);

            prva = new Destinacija("Seul", "Južna Koreja", Kontinenti.Azija);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Kairo", "Egipat", Kontinenti.Afrika);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Adis Abeba", "Etiopija", Kontinenti.Afrika);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Otava", "Kanada", Kontinenti.SjevernaAmerika);
            Globalna.nasaAgencija.Destinacije.Add(prva);
            prva = new Destinacija("Rio de Janeiro", "Brazil", Kontinenti.JuznaAmerika);
            Globalna.nasaAgencija.Destinacije.Add(prva);

            gDestinacija.Visibility = Visibility.Collapsed;
            foreach (var dest in Globalna.nasaAgencija.Destinacije)
            {
                cDestinacije.Items.Add(dest.Naziv);
            }
            cDestinacije.Items.Add("Ništa od ponuđenog");
            gHotel.Visibility = Visibility.Collapsed;
            if(naziviDestinacija.Count == 0)
            {
                naziviDestinacija.Add("Nijedna od ponuđenih");
                naziviHotela.Add("Nijedan od ponuđenih");
            }    

            foreach (var pr in Globalna.nasaAgencija.Prevozi)
            {
                if (pr.VrstaPrevoza.Equals(VrstaPrevoza.autobus)) autobusi.Add(pr.Ime);
            }
        }

        public void popuniKomboBoxove()
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Frame.Navigate(typeof(PocetnaStranica));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rAvion.IsChecked == true)
            {
                cPrevoz.Items.Add(autobusi);
                gLet.Visibility = Visibility.Visible;
            }
            else
            {
                cPrevoz.Items.Add(avioKompanije);
                gLet.Visibility = Visibility.Collapsed;
            }
        }

        private void cDestinacije_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string odabrano = cDestinacije.SelectedItem.ToString();
            if (odabrano.Equals("Ništa od ponuđenog"))
            {
                gDestinacija.Visibility = Visibility.Visible;
            }

            cHoteli.Items.Clear();
            List<string> hoteli = putovanjeVM.dajListuHotelaPoDestinaciji(odabrano);
            foreach (var temp in hoteli) cHoteli.Items.Add(temp);
            cHoteli.Items.Add("Ništa od ponuđenog");
            if (odabrano != "Ništa od ponuđenog")
            {
                gDestinacija.Visibility = Visibility.Collapsed;
            }

            

            /*else
            {
                gDestinacija.Visibility = Visibility.Collapsed;
               // naziviHotela = putovanjeVM.dajListuHotelaPoDestinaciji(naziviDestinacija[cDestinacije.SelectedItem]);
                autobusi = putovanjeVM.dajListuBusevaPoDestinacijiIKapacitetu(naziviDestinacija[cDestinacije.SelectedIndex], Convert.ToInt32(sMax.Value));
                   // avioKompanije
            }*/
            /*  if(cDestinacije.SelectedIndex > -1)
              odabranaDestinacija = naziviDestinacija[cDestinacije.SelectedIndex];*/

        }

        private void cHoteli_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string odabrano = cHoteli.SelectedItem.ToString();
            if (odabrano.Equals("Ništa od ponuđenog")) gHotel.Visibility = Visibility.Visible;
            else gHotel.Visibility = Visibility.Collapsed;

            if (cHoteli.SelectedIndex > -1)
                odabraniHotel = naziviHotela[cHoteli.SelectedIndex];
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { //dodat polje za upis opisa putovanja
            Prevoz prevoz;
            Boolean istaknuto2 = false;
            Boolean jelOK = true;
            if (rAutobus.IsChecked == true)
                prevoz = Globalna.nasaAgencija.Prevozi[cPrevoz.SelectedIndex];
            else
                prevoz = null;

            /*VALIDACIJE PRIJE KREIRANJA PUTOVANJA 
             AKO JE UREDU, VRATI jelOK NA TRUE RADI FINALNE PROVJERE*/

            if (sMin.Value <= sMax.Value)
            {
                jelOK = true;
                greska.Visibility = Visibility.Collapsed;
                minBroj.Foreground = new SolidColorBrush(Colors.Black);
                maxBroj.Foreground = new SolidColorBrush(Colors.Black);
            }
            if (dPolaska.Date < dPovratka.Date)
            {
                jelOK = true;
                greska8.Visibility = Visibility.Collapsed;
            }
           
            if (rDA.IsChecked == true || rNE.IsChecked == true)
            {
                
                greska1.Visibility = Visibility.Collapsed;
                jelOK = true;
            }

            if (!String.IsNullOrEmpty(tCijena.Text))
            {
                jelOK = true;
                greska5.Visibility = Visibility.Collapsed;
            }

            if (cPrevoz.SelectedIndex != -1)
            {
                jelOK = true;
                greska4.Visibility = Visibility.Collapsed;
            }

            if (cHoteli.SelectedIndex != -1)
            {
                jelOK = true;
                greska3.Visibility = Visibility.Collapsed;
            }

            if (rAvion.IsChecked == true)
            {
                if (comboBox.SelectedIndex != -1)
                {
                    jelOK = true;
                    greska6.Visibility = Visibility.Collapsed;
                }

                if (comboBox1.SelectedIndex != -1)
                {
                    jelOK = true;
                    greska7.Visibility = Visibility.Collapsed;
                }
            }
            //AKO NIJE UREDU
            if (sMin.Value > sMax.Value)
            {
                jelOK = false;
                greska.Visibility = Visibility.Visible;
                minBroj.Foreground = new SolidColorBrush(Colors.Red);
                maxBroj.Foreground = new SolidColorBrush(Colors.Red);
            }

            if (rDA.IsChecked == false && rNE.IsChecked == false)
            {
                greska1.Visibility = Visibility.Visible; //AKTIVACIJA textBloc-a KOJI PRIKAZUJE GREŠKU, KAO errorProvider
                jelOK = false;
            }

            if (dPolaska.Date > dPovratka.Date)
            {
                jelOK = false;
                greska8.Visibility = Visibility.Visible;
            }

            if (String.IsNullOrEmpty(tCijena.Text))
            {
                jelOK = false;
                greska5.Visibility = Visibility.Visible;
            }

            if (cPrevoz.SelectedIndex == -1)
            {
                jelOK = false;
                greska4.Visibility = Visibility.Visible;
            }

            if (cHoteli.SelectedIndex == -1)
            {
                jelOK = false;
                greska3.Visibility = Visibility.Visible;
            }

            if (rAvion.IsChecked == true)
            {
                if (comboBox.SelectedIndex == -1)
                {
                    jelOK = false;
                    greska6.Visibility = Visibility.Visible;
                }

                if (comboBox1.SelectedIndex == -1)
                {
                    jelOK = false;
                    greska7.Visibility = Visibility.Visible;
                }
            }
            if (!jelOK)
            {
                var dialog = new MessageDialog("Postoje greške. Popravite pa ponovo kreirajte!");
                dialog.ShowAsync();
            }
            if (jelOK)
            {
                putovanjeVM.kreirajPutovanje(dPolaska.Date.Value.Date, dPovratka.Date.Value.Date, Convert.ToInt32(sMin.Value), Convert.ToInt32(sMax.Value), "opis putovanja", istaknuto, Globalna.prijavljenaAgencijaId, Globalna.nasaAgencija.Destinacije[cDestinacije.SelectedIndex], Globalna.nasaAgencija.Hoteli[cHoteli.SelectedIndex], prevoz, Convert.ToDouble(tCijena.Text));
                var dialog = new MessageDialog("Putovanje uspješno kreirano!");
                dialog.ShowAsync();
            }
           
        }



        private void bDodajHotel_Click(object sender, RoutedEventArgs e)
        {
            putovanjeVM.dodajNoviHotel(tHotel.Text, Convert.ToInt32(sMax.Value), Convert.ToInt32(sMin.Value), Globalna.nasaAgencija.Destinacije[cDestinacije.SelectedIndex], Convert.ToDouble(100), null);
        }

        //UBAČENA VARIJABLA U button-u
       private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            istaknuto = true;
        }
        
        private void rAutobus_Checked(object sender, RoutedEventArgs e)
        {
            List<string> prevozi = putovanjeVM.dajListuBusevaPoDestinacijiIKapacitetu(cDestinacije.SelectedItem.ToString(), Convert.ToInt32(sMax.Value));
            foreach (var temp in prevozi) cPrevoz.Items.Add(temp);
            cPrevoz.Items.Add(avioKompanije);
            gLet.Visibility = Visibility.Collapsed;
        }

        private void RadioButton_Checked_2(object sender, RoutedEventArgs e)
        {
            istaknuto = false;
        }

        private void bDodajDestinaciju_Click_1(object sender, RoutedEventArgs e)
        {
            putovanjeVM.dodajNovuDestinaciju(tDestinacija.Text, "-", Kontinenti.Afrika, null); //kako sliku spasit?

        }

        //NEMAM POJMA, NE RADI 
        private void bUcitajHotel_Click(object sender, RoutedEventArgs e)
        {
           /* string lokacijaSlike = String.Empty;
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files (*.jpg)|*.jpg";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    lokacijaSlike = dialog.FileName;
                    pictureBox1.ImageLocation = lokacijaSlike;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void cPrevoz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void sMax_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            
           
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}