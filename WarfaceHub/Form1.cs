using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace WarfaceHub
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Введите ник в поле ввода", "Ошибка");
            }
            else
            {
                string nickname = textBox1.Text;
                warning.Visible = false;
                stats.Visible = true;
                    HttpWebResponse response = (HttpWebResponse)WebRequest.Create($"http://api.warface.ru/user/stat/?name={nickname}&server=0").GetResponse();
                    JSON playerinfo = JsonConvert.DeserializeObject<JSON>(new StreamReader(response.GetResponseStream()).ReadToEnd());
                    response.Close();

                    string result = @$" 
                    Ник: {playerinfo.nickname},
                    ID: {playerinfo.user_id},
                    Клан: {playerinfo.clan_name},
                    ID клана: {playerinfo.clan_id},
                    Ранг:  {playerinfo.rank_id},
                    Опыт: {playerinfo.experience},
                    Времени в игре: {playerinfo.playtime_h} часов, {playerinfo.playtime_m} минут,
                    PVP:
                    У/С: {playerinfo.pvp},
                    Убийств в общем: {playerinfo.kill},
                    Убийств врагов: {playerinfo.kills},
                    Убийств тиммейтов: {playerinfo.friendly_kills},
                    Смертей: {playerinfo.death},
                    Общее количество ссыграных игр: {playerinfo.pvp_all},
                    Выйграно игр: {playerinfo.pvp_wins},
                    Проиграно игр: {playerinfo.pvp_lost},
                    Любимый класс: {playerinfo.favoritPVP}, 
                    PVE:
                    У/С: {playerinfo.pve},
                    Убийств в общем: {playerinfo.pve_kill},
                    Убийств врагов: {playerinfo.pve_kills},
                    Убийств тиммейтов: {playerinfo.pve_friendly_kills},
                    Смертей: {playerinfo.pve_death},
                    Общее количество ссыграных игр: {playerinfo.pve_all},
                    Выйграно игр: {playerinfo.pve_wins},
                    Проиграно игр: {playerinfo.pve_lost},
                    Любимый класс: {playerinfo.favoritPVE},";

                stats.Text = result;
            }
        }

        class JSON
        {
            public string user_id { get; set; }
            public string nickname { get; set; }
            public int experience { get; set; }
            public int rank_id { get; set; }
            public bool is_transparent { get; set; }
            public int clan_id { get; set; }
            public string clan_name { get; set; }
            public int kill { get; set; }
            public int friendly_kills { get; set; }
            public int kills { get; set; }
            public int death { get; set; }
            public double pvp { get; set; }
            public int pve_kill { get; set; }
            public int pve_friendly_kills { get; set; }
            public int pve_kills { get; set; }
            public int pve_death { get; set; }
            public double pve { get; set; }
            public int playtime { get; set; }
            public int playtime_h { get; set; }
            public int playtime_m { get; set; }
            public string favoritPVP { get; set; }
            public string favoritPVE { get; set; }
            public int pve_wins { get; set; }
            public int pvp_wins { get; set; }
            public int pvp_lost { get; set; }
            public int pve_lost { get; set; }
            public int pve_all { get; set; }
            public int pvp_all { get; set; }
            public float pvpwl { get; set; }
            public string full_response { get; set; }
        }
    }
}
