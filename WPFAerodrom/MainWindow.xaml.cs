using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFAerodrom.Forme;

namespace WPFAerodrom
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ucitanaTabela;
        bool azuriraj;
        Konekcija kon = new Konekcija();
        SqlConnection konekcija = new SqlConnection();

        
        #region Select upiti
        #region Select sa uslovom
        string selectUslovPutnici = @"select * from tbl_Putnik where putnikID=";
        string selectUslovAvioKompanija = @"select * from tbl_AvioKompanija where kompanijaID=";
        string selectUslovPiloti = @"select * from tbl_Pilot where pilotID=";
        string selectUslovAvioni = @"select * from tbl_Avion where avionID=";
        string selectUslovLetovi = @"select * from tbl_Let where letID=";
        string selectUslovPrtljag = @"select * from tbl_Prtljag where prtljagID=";
        string selectUslovOsoblje = @"select * from tbl_Osoblje where osobljeID=";
        string selectUslovTicket = @"select * from tbl_Ticket where ticketID=";
        #endregion

        static string putniciSelect = @"select putnikID as ID, Ime, Prezime, Pasos from tbl_Putnik";
        static string avioKompanijaSelect = @"select kompanijaID as ID, imeKompanije, kontakt, adresa from tbl_AvioKompanija";
        static string pilotiSelect = @"select pilotID as ID, Ime, Prezime, DatumRodjenja , ImeKompanije as Kompanija from tbl_Pilot 
                                       join tbl_AvioKompanija on tbl_Pilot.KompanijaID = tbl_AvioKompanija.KompanijaID;";
        static string avioniSelect = @"select avionID as ID, model, brojMesta , ImeKompanije as Kompanija   from tbl_Avion
                                      join tbl_AvioKompanija on tbl_Avion.KompanijaID = tbl_AvioKompanija.KompanijaID;";
        static string prtljagSelect = @"select PrtljagID as ID, tezina , ime , prezime as Putnik from tbl_Prtljag
                                         join tbl_Putnik on tbl_Prtljag.putnikID = tbl_Putnik.PutnikID;";
        static string letoviSelect = @"select letID as ID, VremePolaska, VremeDolaska , Destinacija , ImeKompanije as Kompanija, Ime as Pilot, Model as avion from tbl_Let
                                       join tbl_AvioKompanija on tbl_Let.KompanijaID = tbl_AvioKompanija.KompanijaID
                                       join tbl_Pilot on tbl_Let.PilotID = tbl_Pilot.PilotID
                                       join tbl_Avion on tbl_Let.AvionID = tbl_Avion.AvionID;";
        static string osobljeSelect = @"select OsobljeID as ID , ime , prezime , zaduzenje , imeKompanije as Kompanija from tbl_Osoblje
                                       join tbl_AvioKompanija on tbl_Osoblje.KompanijaID = tbl_AvioKompanija.KompanijaID;";
        static string ticketSelect = @"select TicketID as ID, ime , prezime as Putnik , VremePolaska, VremeDolaska , Destinacija as Let from tbl_Ticket
                                       join tbl_Putnik on tbl_Ticket.PutnikID = tbl_Putnik.PutnikID
                                       join tbl_Let on tbl_Ticket.LetID = tbl_Let.LetID;";

        #endregion
        #region Delete upiti
        string aviokompanijaDelete = @"Delete from tbl_AvioKompanija where kompanijaID =";
        string avionDelete = @"Delete from tbl_Avion where avionID=";
        string letDelete = @"Delete from tbl_Let where letID=";
        string pilotDelete = @"Delete from tbl_Pilot where pilotID=";
        string prtljagDelete = @"Delete from tbl_Prtljag where prtljagID=";
        string putnikDelete = @"Delete from tbl_Putnik where putnikID=";
        string osobljeDelete = @"Delete from tbl_Osoblje where osobljeID=";
        string ticketDelete = @"Delete from tbl_Ticket where ticketID=";
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            konekcija = kon.KreirajKonekciju();
            UcitajPodatke(dataGridCentralni, putniciSelect);
            
        }
        private void UcitajPodatke(DataGrid grid, string selectUpit)
        {
            try
            {
                konekcija.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectUpit, konekcija);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);
                if (grid != null)
                {
                    grid.ItemsSource = dt.DefaultView;
                }
                ucitanaTabela = selectUpit;
                dt.Dispose();
                dataAdapter.Dispose();
            }
            catch (SqlException)
            {
                MessageBox.Show("Neuspesno uneti podaci", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
            }
        }

        void PopuniFormu(DataGrid grid, string selectUslov)
        {
            try
            {
                konekcija.Open();
                azuriraj = true;
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                SqlCommand cmd = new SqlCommand
                {
                    Connection = konekcija
                };
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                cmd.CommandText = selectUslov + "@id";
                SqlDataReader citac = cmd.ExecuteReader();
                cmd.Dispose();
                if(citac.Read())
                {
                    if (ucitanaTabela.Equals(pilotiSelect))
                    {
                        FrmPilot prozorPilot = new FrmPilot(azuriraj, red);
                        prozorPilot.txtIme.Text = citac["Ime"].ToString();
                        prozorPilot.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorPilot.dpDatum.SelectedDate = (DateTime)citac["DatumRodjenja"];
                        prozorPilot.cbKompanija.SelectedValue = citac["kompanijaID"].ToString();
                        prozorPilot.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(putniciSelect))
                    {
                        FrmPutnik prozorPutnik = new FrmPutnik(azuriraj, red);
                        prozorPutnik.txtIme.Text = citac["Ime"].ToString();
                        prozorPutnik.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorPutnik.txtPasos.Text = citac["Pasos"].ToString();
                        prozorPutnik.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(avioniSelect))
                    {
                        FrmAvion prozorAvion = new FrmAvion(azuriraj, red);
                        prozorAvion.txtNazivModela.Text = citac["Model"].ToString();
                        prozorAvion.txtBrojMesta.Text = citac["BrojMesta"].ToString();
                        prozorAvion.cbKompanija.SelectedValue = citac["kompanijaID"].ToString();
                        prozorAvion.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(avioKompanijaSelect))
                    {
                        FrmAvioKompanija prozorAvioKompanija = new FrmAvioKompanija(azuriraj, red);
                        prozorAvioKompanija.txtIme.Text = citac["ImeKompanije"].ToString();
                        prozorAvioKompanija.txtKontakt.Text = citac["Kontakt"].ToString();
                        prozorAvioKompanija.txtAdresa.Text = citac["Adresa"].ToString();
                        prozorAvioKompanija.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(letoviSelect))
                    {
                        FrmLet prozorLetovi = new FrmLet(azuriraj, red);
                        prozorLetovi.dpDatumP.SelectedDate = (DateTime)citac["VremePolaska"];
                        prozorLetovi.dpDatumD.SelectedDate = (DateTime)citac["VremeDolaska"];
                        prozorLetovi.txtDestinacija.Text = citac["Destinacija"].ToString();
                        prozorLetovi.cbAvion.SelectedValue = citac["avionID"].ToString();
                        prozorLetovi.cbKompanija.SelectedValue = citac["kompanijaID"].ToString();
                        prozorLetovi.cbPilot.SelectedValue = citac["pilotID"].ToString();
                        prozorLetovi.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(prtljagSelect))
                    {
                        FrmPrtljag prozorPrtljag = new FrmPrtljag(azuriraj, red);
                        prozorPrtljag.txtTezinaPrtljaga.Text = citac["Tezina"].ToString();
                        prozorPrtljag.ShowDialog();
                    }
                    else if (ucitanaTabela.Equals(osobljeSelect))
                    {
                        FrmOsoblje prozorOsoblje = new FrmOsoblje(azuriraj, red);
                        prozorOsoblje.txtIme.Text = citac["Ime"].ToString();
                        prozorOsoblje.txtPrezime.Text = citac["Prezime"].ToString();
                        prozorOsoblje.txtZaduzenje.Text = citac["Zaduzenje"].ToString();
                        prozorOsoblje.cbKompanija.SelectedValue = citac["KompanijaID"].ToString();
                        prozorOsoblje.ShowDialog();

                    }
                    else if(ucitanaTabela.Equals(ticketSelect))
                    {
                        FrmTicket prozorTicket = new FrmTicket(azuriraj, red);
                        prozorTicket.cbPutnik.SelectedValue = citac["PutnikID"].ToString();
                        prozorTicket.cbLet.SelectedValue = citac["LetID"].ToString();
                        prozorTicket.ShowDialog();
                    }
                }

            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red!", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (konekcija != null)
                    konekcija.Close();
                azuriraj = false;
            }
        }
        void ObrisiZapis(DataGrid grid, string deleteUpit)
        {
            try
            {
                konekcija.Open();
                DataRowView red = (DataRowView)grid.SelectedItems[0];
                MessageBoxResult rezultat = MessageBox.Show("Da li ste sigurni?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezultat == MessageBoxResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = konekcija
                    };
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = red["ID"];
                    cmd.CommandText = deleteUpit + "@id";
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Niste selektovali red", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("Postoje povezani podaci u nekim drugim tabelama", "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                if (konekcija != null)
                {
                    konekcija.Close();
                }
            }
        }

        private void btnPilot_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, pilotiSelect);
        }

        private void btnPutnik_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, putniciSelect);
        }

        private void btnAvion_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, avioniSelect);
        }


        private void btnAvioKompanija_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, avioKompanijaSelect);
        }

        private void btnLet_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, letoviSelect);
        }

        private void btnPrtljag_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, prtljagSelect);
        }
        private void btnOsoblje_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, osobljeSelect);
        }
        private void btnTicket_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dataGridCentralni, ticketSelect);
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if (ucitanaTabela.Equals(pilotiSelect))
            {
                prozor = new FrmPilot();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, pilotiSelect);
            }
            else if (ucitanaTabela.Equals(putniciSelect))
            {
                prozor = new FrmPutnik();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, putniciSelect);
            }
            else if (ucitanaTabela.Equals(avioniSelect))
            {
                prozor = new FrmAvion();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, avioniSelect);
            }
            else if (ucitanaTabela.Equals(avioKompanijaSelect))
            {
                prozor = new FrmAvioKompanija();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, avioKompanijaSelect);
            }
           else  if (ucitanaTabela.Equals(letoviSelect))
            {
                prozor = new FrmLet();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, letoviSelect);
            }
            else if (ucitanaTabela.Equals(prtljagSelect))
            {
                prozor = new FrmPrtljag();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, prtljagSelect);
            }
            else if (ucitanaTabela.Equals(osobljeSelect))
            {
                prozor = new FrmOsoblje();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, osobljeSelect);
            }
            else if (ucitanaTabela.Equals(ticketSelect))
            {
                prozor = new FrmTicket();
                prozor.ShowDialog();
                UcitajPodatke(dataGridCentralni, ticketSelect);
            }
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(pilotiSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovPiloti);
                UcitajPodatke(dataGridCentralni, pilotiSelect);
            }
            else if (ucitanaTabela.Equals(putniciSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovPutnici);
                UcitajPodatke(dataGridCentralni, putniciSelect);
            }
            else if (ucitanaTabela.Equals(avioniSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovAvioni);
                UcitajPodatke(dataGridCentralni, avioniSelect);
            }
            else if (ucitanaTabela.Equals(avioKompanijaSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovAvioKompanija);
                UcitajPodatke(dataGridCentralni, avioKompanijaSelect);
            }
            else if (ucitanaTabela.Equals(letoviSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovLetovi);
                UcitajPodatke(dataGridCentralni, letoviSelect);
            }
            else if (ucitanaTabela.Equals(prtljagSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovPrtljag);
                UcitajPodatke(dataGridCentralni, prtljagSelect);
            }
            else if (ucitanaTabela.Equals(osobljeSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovOsoblje);
                UcitajPodatke(dataGridCentralni, osobljeSelect);
            }
            else if(ucitanaTabela.Equals(ticketSelect))
            {
                PopuniFormu(dataGridCentralni, selectUslovTicket);
                UcitajPodatke(dataGridCentralni, ticketSelect);
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (ucitanaTabela.Equals(avioKompanijaSelect))
            {
                ObrisiZapis(dataGridCentralni, aviokompanijaDelete);
                UcitajPodatke(dataGridCentralni, avioKompanijaSelect);
            }
            else if(ucitanaTabela.Equals(avioniSelect))
            {
                ObrisiZapis(dataGridCentralni, avionDelete);
                UcitajPodatke(dataGridCentralni, avioniSelect);
            }
            else if(ucitanaTabela.Equals(letoviSelect))
            {
                ObrisiZapis(dataGridCentralni, letDelete);
                UcitajPodatke(dataGridCentralni, letoviSelect);
            }
            else if (ucitanaTabela.Equals(pilotiSelect))
            {
                ObrisiZapis(dataGridCentralni, pilotDelete);
                UcitajPodatke(dataGridCentralni, pilotiSelect);
            }
            else if (ucitanaTabela.Equals(prtljagSelect))
            {
                ObrisiZapis(dataGridCentralni, prtljagDelete);
                UcitajPodatke(dataGridCentralni, prtljagSelect);
            }
            else if(ucitanaTabela.Equals(putniciSelect))
            {
                ObrisiZapis(dataGridCentralni, putnikDelete);
                UcitajPodatke(dataGridCentralni, putniciSelect);
            }
            else if (ucitanaTabela.Equals(osobljeSelect))
            {
                ObrisiZapis(dataGridCentralni, osobljeDelete);
                UcitajPodatke(dataGridCentralni, osobljeSelect);
            }
            else if (ucitanaTabela.Equals(ticketSelect))
            {
                ObrisiZapis(dataGridCentralni, ticketDelete);
                UcitajPodatke(dataGridCentralni, ticketSelect);
            }    
        }

        
    }
}
