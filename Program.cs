using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    // Connection String dipindahkan ke dalam kelas
    static string connectionString = "Data Source=PAVILIONGAME\\YUDHA_PUTRA_RAMA;" +
        "Initial Catalog=Management_Komunitas; Integrated Security=True";

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Aplikasi Manajemen Komunitas ===");
            Console.WriteLine("1. Tambah Data Komunitas");
            Console.WriteLine("2. Tampilkan Data Komunitas");
            Console.WriteLine("3. Hapus Data Komunitas");
            Console.WriteLine("4. Keluar");
            Console.Write("Pilih menu (1-4): ");
            string pilihan = Console.ReadLine();

            switch (pilihan)
            {
                case "1":
                    InsertData();
                    break;
                case "2":
                    RefreshData();
                    break;
                case "3":
                    DeleteData();
                    break;
                case "4":
                    Console.WriteLine("Terima kasih telah menggunakan aplikasi ini.");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid, coba lagi!");
                    break;
            }

            Console.WriteLine("\nTekan ENTER untuk kembali ke menu utama...");
            Console.ReadLine();
        }
    }

    static void InsertData()
    {
        Console.WriteLine("\n=== Tambah Data Komunitas ===");
        Console.Write("Masukkan ID Komunitas (5 digit): ");
        string idkomun = Console.ReadLine();
        Console.Write("Masukkan Nama Komunitas: ");
        string nama = Console.ReadLine();
        Console.Write("Masukkan Nama Admin Komunitas: ");
        string adminkomun = Console.ReadLine();
        Console.Write("Masukkan deskripsi Komunitas: ");
        string deskripsi = Console.ReadLine();
        Console.Write("Masukkan No Telepon (+62xxxxx): ");
        string telepon = Console.ReadLine();
        Console.Write("Masukkan Kategori: ");
        string kategori = Console.ReadLine();
        Console.Write("Masukkan Alamat Komunitas: ");
        string alamat = Console.ReadLine();
        Console.Write("Masukkan Email Komunitas: ");
        string email = Console.ReadLine();
        Console.Write("Masukkan Jumlah Anggota Komunitas: ");
        string jumlah = Console.ReadLine();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Komunitas (IdKomunitas, NamaKomunitas, AdminKomunitas, Deskripsi, NomorTeleponKomunitas, Kategori, AlamatKomunitas, EmailKomunitas, JumlahAnggota) " +
                "VALUES (@id, @Nama, @AdminKomun, @Deskripsi, @Telepon, @Kategori, @Alamat, @Email, @JumlahAnggota)";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", idkomun);
            cmd.Parameters.AddWithValue("@Nama", nama);
            cmd.Parameters.AddWithValue("@AdminKomun", adminkomun);
            cmd.Parameters.AddWithValue("@Deskripsi", deskripsi);
            cmd.Parameters.AddWithValue("@Telepon", telepon);
            cmd.Parameters.AddWithValue("@Kategori", kategori);
            cmd.Parameters.AddWithValue("@Alamat", alamat);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@JumlahAnggota", jumlah);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    Console.WriteLine("Data berhasil ditambahkan!");
                else
                    Console.WriteLine("Gagal menambahkan data.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    static void RefreshData()
    {
        Console.WriteLine("\n=== Daftar Komunitas ===");

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Komunitas";
            SqlCommand cmd = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("\n{0,-5} | {1,-50} | {2,-50} | {3,-100} | {4,-13}, | {5,-25} | {6,-50}, {7,-50}, {8,-10}",
                        "Id", "Nama Komunitas", "Nama Admin", "Deskripsi Komunitas", "No Telepon", "Kategori", "Alamat", "Email", "Jumlah Anggota");
                    Console.WriteLine(new string('-', 110));

                    while (reader.Read())
                    {
                        Console.WriteLine("{0,-5} | {1,-50} | {2,-50} | {3,-100} | {4,-13}, | {5,-25} | {6,-50}, {7,-50}, {8,-10}",
                            reader["Id"], reader["Nama Komunitas"], reader["Nama Admin"], reader["Deskripsi Komunitas"], reader["No Telepon"], reader["Kategori"],
                            reader["Alamat"], reader["Email"], reader["Jumlah Anggota"]);
                    }
                }
                else
                {
                    Console.WriteLine("Tidak ada data komunitas.");
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    static void DeleteData()
    {
        Console.Write("\nMasukkan ID Komunitas yang ingin dihapus: ");
        string idkomun = Console.ReadLine();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Komunitas WHERE IdKomunitas = @id";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", idkomun);

            try
            {
                conn.Open();
                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                    Console.WriteLine("Data berhasil dihapus!");
                else
                    Console.WriteLine("Data tidak ditemukan.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
