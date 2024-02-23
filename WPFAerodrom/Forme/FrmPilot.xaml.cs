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
    /// Interaction logic for FrmPilot.xaml
    /// </summary>
    public partial class FrmPilot : Window
    {
        
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();
        bool azuriraj;
        DataRowView pomocniRed;
        public FrmPilot()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            PopuniPadajuceListe();
            txtIme.Focus();
        }
        public FrmPilot(bool azuriraj, DataRowView pomocniRed)
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            txtIme.Focus();
            PopuniPadajuceListe();
            this.azuriraj = azuriraj;
            this.pomocniRed = pomocniRed;
            konekcija = kon.KreirajKonekciju();
        }
        private void PopuniPadajuceListe()
        {
            try
            {
                konekcija.Open();
                string vratiKompaniju = @"select kompanijaID, imeKompanije from tbl_AvioKompanija";
                DataTable dtKompanija = new DataTable();
                SqlDataAdapter daKompanija = new SqlDataAdapter(vratiKompaniju, konekcija);


                daKompanija.Fill(dtKompanija);

                cbKompanija.ItemsSource = dtKompanija.DefaultView;
                dtKompanija.Dispose();
                dtKompanija.Dispose();

            }
            catch (SqlException)
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
                DateTime date = (DateTime)dpDatum.SelectedDate;
                string datum = date.ToString("yyyy-MM-dd");
           
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                cmd.Parameters.Add("@datumRodjenja", SqlDbType.DateTime).Value = datum;
                cmd.Parameters.Add("@kompanijaID", SqlDbType.Int).Value = int.Parse(((DataRowView)cbKompanija.SelectedItem).Row["KompanijaID"].ToString());// cbKompanija;

                if (this.azuriraj)
                {
                    
                    DataRowView red = this.pomocniRed;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = @"Update tbl_Pilot set Ime = @ime, Prezime = @prezime, DatumRodjenja = @datumRodjenja, KompanijaID=@kompanijaID where PilotID = @id";
                    this.azuriraj = false;
                }
                else
                {
                    cmd.CommandText = "insert into tbl_Pilot(Ime,Prezime,DatumRodjenja,KompanijaID) values(@ime, @prezime, @datumRodjenja, @kompanijaID)";
                }

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                this.Close();
            }
            catch(SqlException sqlEx)
            {
                MessageBox.Show("Unos odredjenih vrednosti nije validan SQL greska: \n" + sqlEx.Message, "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            catch(InvalidOperationException)
            {
                MessageBox.Show("Odaberite datum", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(FormatException)
            {
                MessageBox.Show("Greska prilikom konverzije podataka!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
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
