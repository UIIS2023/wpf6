using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFAerodrom.Forme
{
    /// <summary>
    /// Interaction logic for FrmLet.xaml
    /// </summary>
    public partial class FrmLet : Window
    {
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmLet()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            dpDatumP.Focus();

        }
        public FrmLet(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            dpDatumP.Focus();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
        }
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiAvione = @"select avionID, model from tbl_Avion";
                DataTable dtAvion = new DataTable();
                SqlDataAdapter daAvion = new SqlDataAdapter(vratiAvione, konekcija);
                daAvion.Fill(dtAvion);
                cbAvion.ItemsSource = dtAvion.DefaultView;
                dtAvion.Dispose();
                dtAvion.Dispose();

                string vratiKompaniju = @"select kompanijaID, imeKompanije from tbl_AvioKompanija";
                DataTable dtKompanija = new DataTable();
                SqlDataAdapter daKompanija = new SqlDataAdapter(vratiKompaniju, konekcija);
                daKompanija.Fill(dtKompanija);
                cbKompanija.ItemsSource = dtKompanija.DefaultView;
                dtKompanija.Dispose();
                dtKompanija.Dispose();

                string vratiPilota = @"select pilotID, Ime + ' ' + Prezime + ' ' as Pilot  from tbl_Pilot";
                DataTable dtPilot = new DataTable();
                SqlDataAdapter daPilot = new SqlDataAdapter(vratiPilota, konekcija);
                daPilot.Fill(dtPilot);
                cbPilot.ItemsSource = dtPilot.DefaultView;
                dtPilot.Dispose();
                dtPilot.Dispose();
            }
            catch(SqlException)
            {
                MessageBox.Show("Padajuce liste nisu popunjene", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                konekcija.Open();
                DateTime date = (DateTime)dpDatumP.SelectedDate;
                string datum = date.ToString("dd-MM-yyyy");
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@vremePolaska", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@vremeDolaska", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@destinacija", SqlDbType.Text).Value = txtDestinacija.Text;
                cmd.Parameters.Add("@avionID", SqlDbType.Int).Value = int.Parse(((DataRowView)cbAvion.SelectedItem).Row["AvionID"].ToString());
                cmd.Parameters.Add("@kompanijaID", SqlDbType.Int).Value = int.Parse(((DataRowView)cbKompanija.SelectedItem).Row["KompanijaID"].ToString());
                cmd.Parameters.Add("@pilotID", SqlDbType.Int).Value = int.Parse(((DataRowView)cbPilot.SelectedItem).Row["PilotID"].ToString());
                if (this.azuriraj)
                {
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["id"];
                    cmd.CommandText = @"Update tbl_Let set vremePolaska = @vremePolaska, vremeDolaska = @vremeDolaska, destinacija = @destinacija, AvionID = @avionID, KompanijaID = @kompanijaID, PilotID = @pilotID where letID = @id";
                    this.azuriraj = false;
                }
                else
                {
                    cmd.CommandText = "insert into tbl_Let(VremePolaska, VremeDolaska, Destinacija, AvionID, KompanijaID, PilotID) values(@vremePolaska, @vremeDolaska, @Destinacija, @avionID, @kompanijaID, @pilotID)";
                }

                
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch(SqlException)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }


        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
