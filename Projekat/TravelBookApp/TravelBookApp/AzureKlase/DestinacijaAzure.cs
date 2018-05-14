﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBookApp.Model;

namespace TravelBookApp.AzureKlase
{
    public class DestinacijaAzure
    {
        public String id { get; set; }
        public String naziv { get; set; }
        public String drzava { get; set; }
        public String kontinent { get; set; }
        public String slika { get; set; }

        public DestinacijaAzure() { }
        public DestinacijaAzure(String _id, String _naziv, String _drzava, String _kontinent, String _slika = "")
        {
            id = _id;
            naziv = _naziv;
            drzava = _drzava;
            kontinent = _kontinent;
            slika = _slika;
        }

        public void UcitajDestinacije() {
            try
            {

                string query = "SELECT * FROM DestinacijaAzure;";
                ConnectionStringAzure s = new ConnectionStringAzure();
                using (SqlConnection c = new SqlConnection(s.konekcija))
                {
                    c.Open();
                    if (c.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand sc = c.CreateCommand();
                        sc.CommandText = query;
                        SqlDataReader reader = sc.ExecuteReader();
                        while (reader.Read())
                        {
                            Kontinenti kont = (Kontinenti)Enum.Parse(typeof(Kontinenti), reader.GetString(3));
                            Destinacija d = new Destinacija(reader.GetString(1), reader.GetString(2), kont);
                            Globalna.nasaAgencija.Destinacije.Add(d);
                        }
                    }
                    c.Close();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);

            }
        }

        public int dodajDestinaciju(Destinacija d) {
            try
            {
                String query = "insert into DestinacijaAzure values (@id,@naziv,@drzava,@kontinent,@slika)";
                ConnectionStringAzure s = new ConnectionStringAzure();
                using (SqlConnection con = new SqlConnection(s.konekcija))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = query;

                    SqlParameter id = new SqlParameter();
                    id.Value = d.Id;
                    id.ParameterName = "id";
                    cmd.Parameters.Add(id);

                    SqlParameter naziv = new SqlParameter();
                    naziv.Value = d.Naziv;
                    naziv.ParameterName = "naziv";
                    cmd.Parameters.Add(naziv);

                    SqlParameter drzava = new SqlParameter();
                    drzava.Value = d.Drzava;
                    drzava.ParameterName = "drzava";
                    cmd.Parameters.Add(drzava);

                    SqlParameter kontinent = new SqlParameter();
                    kontinent.Value = d.Kontinent;
                    kontinent.ParameterName = "kontinent";
                    cmd.Parameters.Add(kontinent);

                    SqlParameter slika = new SqlParameter();
                    slika.Value = d.SlikeDestinacije;
                    slika.ParameterName = "slika";
                    cmd.Parameters.Add(slika);

                    con.Open();
                    int r = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    return r;

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message);
                return 0;
            }
        }
    }
}