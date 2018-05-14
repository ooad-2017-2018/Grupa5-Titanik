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
    public class HotelAzure
    {
        public string id { get; set; }
        public string ime { get; set; }
        public int maxKapacitet { get; set; }
        public int kapacitet { get; set; }
        public string idDestinacije { get; set; }
        public double cijena { get; set; }
        public string slika { get; set; }

        public HotelAzure() { }

        public HotelAzure(string _id, string _ime, int _maxKapacitet, int _kapacitet, string _idDestinacije, double _cijena, string _slika = "")
        {
            id = _id;
            ime = _ime;
            slika = _slika;
            maxKapacitet = _maxKapacitet;
            kapacitet = _kapacitet;
            idDestinacije = _idDestinacije;
            cijena = _cijena;
        }

        public void UcitajHotele()
        {
            try
            {
                string query = "SELECT * FROM HotelAzure;";
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
                            int index = -1;
                            for (int i = 0; i < Globalna.nasaAgencija.Destinacije.Count; i++)
                            {
                                if (Globalna.nasaAgencija.Destinacije[i].ToString() == reader.GetString(4))
                                {
                                    index = i;
                                    break;
                                }
                            }
                            Hotel h = new Hotel(reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3), Globalna.nasaAgencija.Destinacije[index], reader.GetDouble(5));
                            Globalna.nasaAgencija.Hoteli.Add(h);
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

        public int dodajHotel(Hotel h)
        {
            try
            {
                String query = "insert into HotelAzure values (@id,@ime,@maxKapacitet,@kapacitet,@idDestinacije,@cijena,@slika)";
                ConnectionStringAzure s = new ConnectionStringAzure();
                using (SqlConnection con = new SqlConnection(s.konekcija))
                {
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = query;

                    SqlParameter id = new SqlParameter();
                    id.Value = h.Id;
                    id.ParameterName = "id";
                    cmd.Parameters.Add(id);

                    SqlParameter ime = new SqlParameter();
                    ime.Value = h.Ime;
                    ime.ParameterName = "ime";
                    cmd.Parameters.Add(ime);

                    SqlParameter maxKapacitet = new SqlParameter();
                    maxKapacitet.Value = h.MaximalniKapacitet;
                    maxKapacitet.ParameterName = "maxKapacitet";
                    cmd.Parameters.Add(maxKapacitet);

                    SqlParameter kapacitet = new SqlParameter();
                    kapacitet.Value = h.Kapacitet;
                    kapacitet.ParameterName = "kapacitet";
                    cmd.Parameters.Add(kapacitet);

                    SqlParameter idDestinacije = new SqlParameter();
                    idDestinacije.Value = h.Lokacija;
                    idDestinacije.ParameterName = "idDestinacije";
                    cmd.Parameters.Add(idDestinacije);

                    SqlParameter cijena = new SqlParameter();
                    cijena.Value = h.CijenaPoOsobi;
                    cijena.ParameterName = "cijena";
                    cmd.Parameters.Add(cijena);

                    SqlParameter slika = new SqlParameter();
                    slika.Value = h.SlikeHotela;
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
