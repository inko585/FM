using FM.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fm_manager
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            nationBindingSource.DataSource = Program.Nations;
            ethnieBindingSource.DataSource = Program.Ethnies;
            associationBindingSource.DataSource = Program.Associations;
            lookAssociationBindingSource.DataSource = Program.AssociationLooks;
            lookPlayerBindingSource.DataSource = Program.PlayerLooks;
            ethnieDeleteButton.Enabled = false;
            nationDeleteButton.Enabled = false;
            associationDeleteBtn.Enabled = false;
            associationDeleteBtn.Enabled = false;
            lookAssociationDeleteButton.Enabled = false;
            lookPlayerDeleteButton.Enabled = false;
            ResetEditor();

        }

        private BindingSource nationBindingSource = new BindingSource();
        private BindingSource subnationBindingSource = new BindingSource();
        private BindingSource ethnieBindingSource = new BindingSource();
        private BindingSource subethnieBindingSource = new BindingSource();
        private BindingSource associationBindingSource = new BindingSource();
        private BindingSource lookAssociationBindingSource = new BindingSource();
        private BindingSource lookPlayerBindingSource = new BindingSource();

        private void ResetEditor()
        {
            nationBindingSource.DataSource = Program.Nations;
            ethnieBindingSource.DataSource = Program.Ethnies;
            lookAssociationBindingSource.DataSource = Program.AssociationLooks;
            lookPlayerBindingSource.DataSource = Program.PlayerLooks;
            nationSelection.DataSource = nationBindingSource.DataSource;
            ethnieSelection.DataSource = ethnieBindingSource.DataSource;
            associationSelection.DataSource = associationBindingSource.DataSource;
            mainEthnieSelection.DataSource = ethnieBindingSource.DataSource;
            associationLookSelection.DataSource = lookAssociationBindingSource.DataSource;
            playerLookSelection.DataSource = lookPlayerBindingSource.DataSource;
            nationSelection.DisplayMember = "Name";
            nationSelection.ValueMember = "Name";
            ethnieSelection.DisplayMember = "Name";
            ethnieSelection.ValueMember = "Name";
            associationSelection.DisplayMember = "Name";
            associationSelection.ValueMember = "Name";
            mainEthnieSelection.ValueMember = "Name";
            mainEthnieSelection.DisplayMember = "Name";
            associationLookSelection.DisplayMember = "Name";
            associationLookSelection.ValueMember = "Name";
            playerLookSelection.DisplayMember = "Name";
            playerLookSelection.ValueMember = "Name";
            ResetNationGrids();
            ResetEthnieGrids();
            ResetAssociationGrids();
            ResetAssociationLookGrids();
            ResetPlayerLookGrids();
        }


        private void SetUpDropDownOccurrenceGridView(DataGridView gridView, string textName, string scaleName, Type t, BindingSource dd_elements, IEnumerable<Occurrence> occurrences)
        {

            gridView.Rows.Clear();
            gridView.Columns.Clear();

            DataGridViewComboBoxColumn dropDown = new DataGridViewComboBoxColumn();
            dropDown.DataSource = dd_elements.DataSource;
            dropDown.HeaderText = textName;
            dropDown.DisplayMember = "Name";
            dropDown.ValueMember = "Name";
            dropDown.ValueType = t;
            dropDown.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            gridView.Columns.Add(dropDown);

            AddNumberDropDown(gridView, scaleName, 1, 1, 10);

            gridView.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView_DefaultValuesNeeded);


            foreach (var o in occurrences)
            {
                gridView.Rows.Add(o.Text, o.ScaleValue);
            }
        }

        private void dataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = 1;
        }

        private void ethnieGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = 1;
            e.Row.Cells[2].Value = 2;
            e.Row.Cells[3].Value = 1;
            e.Row.Cells[4].Value = 1;
        }

        private List<Occurrence> LoadFromGridView(DataGridView gridView)
        {
            var ret = new List<Occurrence>();
            foreach (DataGridViewRow r in gridView.Rows)
            {
                if (r.Cells[0].Value != null && !r.Cells[0].Value.Equals(""))
                    ret.Add(new Occurrence()
                    {
                        Text = (string)r.Cells[0].Value,
                        ScaleValue = (int)r.Cells[1].Value
                    });
            }

            return ret;
        }

        private List<ColorPairOccurrence> LoadFromColorPairGridView(DataGridView gridView)
        {
            var ret = new List<ColorPairOccurrence>();
            foreach (DataGridViewRow r in gridView.Rows)
            {
                if (r.Cells[0].Value != null && !r.Cells[0].Value.Equals(""))
                    ret.Add(new ColorPairOccurrence()
                    {
                        Text = (string)r.Cells[0].Value,
                        Text2 = (string)r.Cells[1].Value,
                        ScaleValue = (int)r.Cells[2].Value
                    });
            }

            return ret;
        }

        private List<PlayerLookOccurrence> LoadFromPlayerLookGridView(DataGridView gridView)
        {
            var ret = new List<PlayerLookOccurrence>();
            foreach (DataGridViewRow r in gridView.Rows)
            {
                if (r.Cells[0].Value != null && !r.Cells[0].Value.Equals(""))
                    ret.Add(new PlayerLookOccurrence()
                    {
                        PlayerLook = (PlayerLook)r.Cells[0].Value,
                        ScaleValue = (int)r.Cells[1].Value,
                    });
            }

            return ret;

        }

        private List<SponsorOccurrence> LoadFromSponsorGridView(DataGridView gridView)
        {
            var ret = new List<SponsorOccurrence>();
            foreach (DataGridViewRow r in gridView.Rows)
            {
                if (r.Cells[0].Value != null && !r.Cells[0].Value.Equals(""))
                    ret.Add(new SponsorOccurrence()
                    {
                        Text = (string)r.Cells[0].Value,
                        ScaleValue = (int)r.Cells[2].Value,
                        Size = (int)r.Cells[1].Value,
                    });
            }

            return ret;
        }

        private List<SubEthnieOccurrence> LoadFromEthnieGridView(DataGridView gridView)
        {
            var ret = new List<SubEthnieOccurrence>();
            foreach (DataGridViewRow r in gridView.Rows)
            {
                if (r.Cells[0].Value != null && !r.Cells[0].Value.Equals(""))
                    ret.Add(new SubEthnieOccurrence()
                    {
                        Text = (string)r.Cells[0].Value,
                        ScaleValue = (int)r.Cells[1].Value,
                        FirstAndLastNamesRate = (int)r.Cells[2].Value,
                        FirstNameRate = (int)r.Cells[3].Value,
                        LastNameRate = (int)r.Cells[4].Value,
                    });
            }

            return ret;
        }


        private void SetUpEthnieOccurrenceGridView(DataGridView gridView, IEnumerable<SubEthnieOccurrence> occurrences)
        {
            SetUpDropDownOccurrenceGridView(gridView, "Weitere Ethnie", "Häufigkeit", typeof(Ethnie), subethnieBindingSource, occurrences);
            gridView.Rows.Clear();
            gridView.Rows[0].Cells[1].Value = 1;
            AddNumberDropDown(gridView, "V+N", 2, 0, 10);
            AddNumberDropDown(gridView, "V", 1, 0, 10);
            AddNumberDropDown(gridView, "N", 1, 0, 10);
            gridView.DefaultValuesNeeded += new DataGridViewRowEventHandler(ethnieGridView_DefaultValuesNeeded);

            foreach (var o in occurrences)
            {
                gridView.Rows.Add(o.Text, o.ScaleValue, o.FirstAndLastNamesRate, o.FirstNameRate, o.LastNameRate);
            }

        }

        private void AddNumberDropDown(DataGridView gridView, string name, int defaultValue, int min, int max)
        {
            DataGridViewComboBoxColumn dropDown = new DataGridViewComboBoxColumn();
            var vals = new List<int>();
            for (int i = min; i <= max; i++)
            {
                vals.Add(i);
            }
            dropDown.DataSource = vals;
            dropDown.ValueType = typeof(int);
            dropDown.HeaderText = name;
            dropDown.DataPropertyName = name;
            dropDown.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            dropDown.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            gridView.Columns.Add(dropDown);
            gridView.AutoResizeColumn(gridView.Columns.Count - 1);
            gridView.Rows[0].Cells[gridView.Rows[0].Cells.Count - 1].Value = defaultValue;
        }

        private void SetUpOccurrenceGridView(DataGridView gridView, string textName, string scaleName, IEnumerable<Occurrence> occurrences)
        {
            gridView.Rows.Clear();
            gridView.Columns.Clear();

            gridView.Columns.Add(textName, textName);
            AddNumberDropDown(gridView, scaleName, 1, 1, 10);
            gridView.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView_DefaultValuesNeeded);

            foreach (var o in occurrences)
            {
                gridView.Rows.Add(o.Text, o.ScaleValue);
            }

        }

        private void SetUpSponsorOccurenceGridView(DataGridView gridView, string textName, string sizeName, string scaleName, IEnumerable<SponsorOccurrence> occurrences)
        {
            gridView.Rows.Clear();
            gridView.Columns.Clear();
            gridView.Columns.Add(textName, textName);
            AddNumberDropDown(gridView, sizeName, 1, 1, 3);
            AddNumberDropDown(gridView, scaleName, 1, 1, 10);
            gridView.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView_DefaultValuesNeeded);

            foreach (var o in occurrences)
            {
                gridView.Rows.Add(o.Text, o.Size, o.ScaleValue);
            }
        }

        private void SetUpColorPairOccurrenceGridView(DataGridView gridView, string textName, string text2Name, string scaleName, IEnumerable<ColorPairOccurrence> occurrences)
        {
            gridView.Rows.Clear();
            gridView.Columns.Clear();

            gridView.Columns.Add(textName, textName);
            gridView.Columns.Add(text2Name, text2Name);
            AddNumberDropDown(gridView, scaleName, 1, 1, 10);
            gridView.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView_DefaultValuesNeeded);

            foreach (var o in occurrences)
            {
                gridView.Rows.Add(o.Text, o.Text2, o.ScaleValue);
            }

        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Export();
            MessageBox.Show("Datensatz gespeichert!");
        }

        private Nation SelectedNation = null;
        private Ethnie SelectedEthnie = null;
        private Association SelectedAssociation = null;
        private AssociationLook SelectedAssociationLook = null;
        private PlayerLook SelectedPlayerLook = null;


        private void nationApply_Click(object sender, EventArgs e)
        {
            var c = SelectedNation == null ? new Nation() : SelectedNation;

            if (nationTextBox.Text.Trim().Equals("") || nationShortTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Die Nation braucht einen Namen und ein Kürzel");
            }
            else
            {
                c.Cities = LoadFromGridView(cityGrid);
                c.FirstPrefixes = LoadFromGridView(prefix1Grid);
                c.SecondPrefixes = LoadFromGridView(prefix2Grid);
                c.Suffixes = LoadFromGridView(suffixGrid);
                c.Sponsors = LoadFromSponsorGridView(sponsorGrid);
                c.Name = nationTextBox.Text;
                c.SubNations = LoadFromGridView(subnationGrid);
                c.MainEthnie = ((Ethnie)mainEthnieSelection.SelectedItem).Name;
                c.SubEthnies = LoadFromEthnieGridView(ethnieGrid).Where(subet => !subet.Text.Equals(c.MainEthnie)).ToList();
                c.Short = nationShortTextBox.Text;
                c.CombineSuffixAndPrefix = suffixPrefixCombo.Checked;

                if (SelectedNation == null)
                {
                    Program.Nations.Add(c);
                }

                ResetNationEditor();
            }

        }

        private void splitContainer6_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ResetNationGrids()
        {
            //todo: fix redundancy
            SetUpEthnieOccurrenceGridView(ethnieGrid, new List<SubEthnieOccurrence>());
            SetUpOccurrenceGridView(cityGrid, "Stadt", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(prefix1Grid, "Vereins Präfix I", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(prefix2Grid, "Vereins Präfix II", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(suffixGrid, "Vereins Suffix", "Häufigkeit", new List<Occurrence>());
            SetUpSponsorOccurenceGridView(sponsorGrid, "Sponsor", "Größe", "Häufigkeit", new List<SponsorOccurrence>());
            SetUpDropDownOccurrenceGridView(subnationGrid, "Ausländer", "Häufigkeit", typeof(Nation), subnationBindingSource, new List<Occurrence>());
        }

        private void ResetEthnieGrids()
        {
            SetUpOccurrenceGridView(firstNameGrid, "Vorname", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(lastNameGrid, "Nachname", "Häufigkeit", new List<Occurrence>());
            SetUpDropDownOccurrenceGridView(playerLookGrid, "Spieler Aussehen", "Häufigkeit", typeof(PlayerLook), lookPlayerBindingSource, new List<Occurrence>());
        }

        private void ResetAssociationGrids()
        {
            SetUpDropDownOccurrenceGridView(nationGrid, "Nation", "Häufigkeit", typeof(Nation), nationBindingSource, new List<Occurrence>());
        }

        private void ResetAssociationLookGrids()
        {
            SetUpColorPairOccurrenceGridView(colorGrid, "Hauptfarbe", "Nebenfarbe", "Häufigkeit", new List<ColorPairOccurrence>());
            SetUpOccurrenceGridView(crestGrid, "Wappen", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(dressGrid, "Trikot", "Häufigkeit", new List<Occurrence>());
        }

        private void ResetPlayerLookGrids()
        {
            SetUpOccurrenceGridView(skinColorGrid, "Hautfarben", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(hairColorGrid, "Haarfarben", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(eyeColorGrid, "Augenfarben", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(headGrid, "Kopf & Haare", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(mouthGrid, "Mund & Bart", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(eyeGrid, "Augen & Brauen", "Häufigkeit", new List<Occurrence>());

        }

        private void ResetNationEditor()
        {
            ResetNationGrids();
            SelectedNation = null;
            nationSelection.SelectedItem = null;
            mainEthnieSelection.SelectedItem = null;
            nationTextBox.Text = "";
            nationShortTextBox.Text = "";
            nationDeleteButton.Enabled = false;
            subnationBindingSource.DataSource = new BindingList<Nation>();
        }

        private void ResetEthnieEditor()
        {
            ResetEthnieGrids();
            SelectedEthnie = null;
            ethnieSelection.SelectedItem = null;
            ethnieTextBox.Text = "";
            ethnieDeleteButton.Enabled = false;

        }

        private void ResetAssociationLookEditor()
        {
            ResetAssociationLookGrids();
            SelectedAssociationLook = null;
            associationLookSelection.SelectedItem = null;
            lookAssociationTextBox.Text = "";
            lookAssociationDeleteButton.Enabled = false;

        }

        private void ResetPlayerLookEditor()
        {
            ResetPlayerLookGrids();
            SelectedPlayerLook = null;
            playerLookSelection.SelectedItem = null;
            lookPlayerTextBox.Text = "";
            lookPlayerDeleteButton.Enabled = false;

        }

        private void ResetAssociationEditor()
        {
            ResetAssociationGrids();
            SelectedAssociation = null;
            associationSelection.SelectedItem = null;
            associationTextBox.Text = "";
            associatonLevel.Value = 0;
            associatonDepth.Value = 0;
        }

        private void LoadNation(Nation n)
        {
            if (n != null)
            {
                nationTextBox.Text = n.Name;
                nationShortTextBox.Text = n.Short;
                nationDeleteButton.Enabled = true;
                suffixPrefixCombo.Checked = n.CombineSuffixAndPrefix;
                SelectedNation = n;
                var bList = new BindingList<Nation>();
                subnationBindingSource.DataSource = bList;
                foreach (var nat in Program.Nations)
                {
                    if (nat != n)
                    {
                        bList.Add(nat);
                    }
                }
                var bList2 = new BindingList<Ethnie>();
                subethnieBindingSource.DataSource = bList2;
                foreach (var et in Program.Ethnies)
                {
                    if (et.Name != n.MainEthnie)
                    {
                        bList2.Add(et);
                    }
                }
                mainEthnieSelection.SelectedItem = Program.Ethnies.FirstOrDefault(et => et.Name == n.MainEthnie);
                SetUpEthnieOccurrenceGridView(ethnieGrid, n.SubEthnies);
                SetUpOccurrenceGridView(cityGrid, "Stadt", "Häufigkeit", n.Cities);
                SetUpOccurrenceGridView(prefix1Grid, "Vereins Präfix I", "Häufigkeit", n.FirstPrefixes);
                SetUpOccurrenceGridView(prefix2Grid, "Vereins Präfix II", "Häufigkeit", n.SecondPrefixes);
                SetUpOccurrenceGridView(suffixGrid, "Vereins Suffix", "Häufigkeit", n.Suffixes);
                SetUpSponsorOccurenceGridView(sponsorGrid, "Sponsor", "Größe", "Häufigkeit", n.Sponsors);
                SetUpDropDownOccurrenceGridView(subnationGrid, "Ausländer", "Häufigkeit", typeof(Nation), subnationBindingSource, n.SubNations);
            }
        }

        private void LoadEthnie(Ethnie e)
        {
            if (e != null)
            {
                ethnieTextBox.Text = e.Name;
                ethnieDeleteButton.Enabled = true;
                SelectedEthnie = e;
                SetUpOccurrenceGridView(firstNameGrid, "Vorname", "Häufigkeit", e.FirstNames);
                SetUpOccurrenceGridView(lastNameGrid, "Nachname", "Häufigkeit", e.LastNames);
                SetUpDropDownOccurrenceGridView(playerLookGrid, "Spieler Aussehen", "Häufigkeit", typeof(PlayerLook), lookPlayerBindingSource, e.Appearences);
            }
        }

        private void LoadAssociationLook(AssociationLook l)
        {
            if (l != null)
            {
                lookAssociationTextBox.Text = l.Name;
                lookAssociationDeleteButton.Enabled = true;
                SelectedAssociationLook = l;
                SetUpColorPairOccurrenceGridView(colorGrid, "Hauptfarbe", "Nebenfarbe", "Häufigkeit", l.ColorPairs);
                SetUpOccurrenceGridView(crestGrid, "Wappen", "Häufigkeit", l.Crests);
                SetUpOccurrenceGridView(dressGrid, "Trikots", "Häufigkeit", l.Dresses);
            }
        }

        private void LoadPlayerLook(PlayerLook l)
        {
            if (l != null)
            {
                lookPlayerTextBox.Text = l.Name;
                lookPlayerDeleteButton.Enabled = true;
                SelectedPlayerLook = l;
                SetUpOccurrenceGridView(skinColorGrid, "Hautfarbe", "Häufigkeit", l.SkinColors);
                SetUpOccurrenceGridView(hairColorGrid, "Haarfarbe", "Häufigkeit", l.HairColors);
                SetUpOccurrenceGridView(eyeColorGrid, "Augenfarbe", "Häufigkeit", l.EyeColors);
                SetUpOccurrenceGridView(headGrid, "Kopf & Haare", "Häufigkeit", l.Heads);
                SetUpOccurrenceGridView(mouthGrid, "Mund & Bart", "Häufigkeit", l.Mouths);
                SetUpOccurrenceGridView(eyeGrid, "Augen & Brauen", "Häufigkeit", l.Eyes);
            }
        }

        private void LoadAssociation(Association a)
        {
            if (a != null)
            {
                associationTextBox.Text = a.Name;
                associationDeleteBtn.Enabled = true;
                SelectedAssociation = a;
                SetUpDropDownOccurrenceGridView(nationGrid, "Nation", "Häufigkeit", typeof(Nation), nationBindingSource, a.Nations);
                associatonLevel.Value = (decimal)a.Power;
                associatonDepth.Value = a.Depth;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetNationEditor();
        }

        private void nationSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadNation((Nation)nationSelection.SelectedItem);
        }

        private void ethnieSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEthnie((Ethnie)ethnieSelection.SelectedItem);
        }

        private void associationSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAssociation((Association)associationSelection.SelectedItem);
        }

        private void ethnieApplyButton_Click(object sender, EventArgs e)
        {
            var et = SelectedEthnie == null ? new Ethnie() : SelectedEthnie;

            if (ethnieTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Die Ethnie braucht einen Namen");
            }
            else
            {
                et.Name = ethnieTextBox.Text;
                et.FirstNames = LoadFromGridView(firstNameGrid);
                et.LastNames = LoadFromGridView(lastNameGrid);
                et.Appearences = LoadFromGridView(playerLookGrid);

                if (SelectedEthnie == null)
                {
                    Program.Ethnies.Add(et);
                }
                
                ResetEthnieEditor();
            }


        }

        private void leagueOkButton_Click(object sender, EventArgs e)
        {
            var a = SelectedAssociation == null ? new Association() : SelectedAssociation;
            if (associationTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Der Verband braucht einen Namen");
            }
            else
            {
                if (associatonLevel.Value < 1 || associatonLevel.Value > 10)
                {
                    MessageBox.Show("Der Verband braucht ein Level > 0");
                }
                else
                {
                    if (associatonDepth.Value < 1 || associatonLevel.Value > 10)
                    {
                        MessageBox.Show("Der Verband braucht eine Ligen Zahl > 0");
                    }
                    else
                    {
                        a.Name = associationTextBox.Text;
                        a.Power = (double)associatonLevel.Value;
                        a.Depth = (int)associatonDepth.Value;
                        a.Nations = LoadFromGridView(nationGrid);

                        if (SelectedAssociation == null)
                        {
                            Program.Associations.Add(a);
                        }

                        ResetAssociationEditor();
                    }
                }
            }



        }

        private void DeleteNation()
        {
            Program.Nations.Remove(SelectedNation);
            ResetNationEditor();
        }

        private void DeleteEthnie()
        {
            Program.Ethnies.Remove(SelectedEthnie);
            ResetEthnieEditor();
        }

        private void DeleteAssociationLook()
        {
            Program.AssociationLooks.Remove(SelectedAssociationLook);
            ResetAssociationLookEditor();
        }

        private void DeletePlayerLook()
        {
            Program.PlayerLooks.Remove(SelectedPlayerLook);
            ResetPlayerLookEditor();
        }


        private void DeleteAssociation()
        {
            Program.Associations.Remove(SelectedAssociation);
            ResetAssociationEditor();
        }

        private void ethnieResetButton_Click(object sender, EventArgs e)
        {
            ResetEthnieEditor();
        }
        private void leagueResetBtn_Click(object sender, EventArgs e)
        {
            ResetAssociationEditor();
        }

        private void ethnieDeleteButton_Click(object sender, EventArgs e)
        {
            DeleteEthnie();
        }

        private void nationDeleteButton_Click(object sender, EventArgs e)
        {
            DeleteNation();
        }

        private void associationDeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteAssociation();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Import();
            ResetEditor();
            ResetEthnieEditor();
            ResetNationEditor();
            ResetAssociationLookEditor();
            ResetAssociationEditor();
            ResetPlayerLookEditor();
            MessageBox.Show("Datensatz importiert!");
        }


        private void associationLookDeleteButton_Click(object sender, EventArgs e)
        {
            DeleteAssociationLook();
        }

        private void associationLookResetButton_Click(object sender, EventArgs e)
        {
            ResetAssociationLookEditor();
        }

        private void associationLookOkButton_Click(object sender, EventArgs e)
        {
            var l = SelectedAssociationLook == null ? new AssociationLook() : SelectedAssociationLook;

            if (lookAssociationTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Das Vereinsaussehen braucht einen Namen");
            }
            else
            {
                l.Name = lookAssociationTextBox.Text;
                l.ColorPairs = LoadFromColorPairGridView(colorGrid);
                l.Crests = LoadFromGridView(crestGrid);
                l.Dresses = LoadFromGridView(dressGrid);

                if (SelectedAssociationLook == null)
                {
                    Program.AssociationLooks.Add(l);
                }

                ResetAssociationLookEditor();
            }
        }

        private void associationLookSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAssociationLook((AssociationLook)associationLookSelection.SelectedItem);
        }

        private void lookPlayerSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPlayerLook((PlayerLook)playerLookSelection.SelectedItem);
        }

        private void lookPlayerDeleteButton_Click(object sender, EventArgs e)
        {
            DeletePlayerLook();
        }

        private void lookPlayerResetButton_Click(object sender, EventArgs e)
        {
            ResetPlayerLookEditor();
        }

        private void lookPlayerOkButton_Click(object sender, EventArgs e)
        {
            var l = SelectedPlayerLook == null ? new PlayerLook() : SelectedPlayerLook;

            if (lookPlayerTextBox.Text.Trim().Equals(""))
            {
                MessageBox.Show("Das Spieleraussehen braucht einen Namen");
            }
            else
            {
                l.Name = lookPlayerTextBox.Text;
                l.SkinColors = LoadFromGridView(skinColorGrid);
                l.HairColors = LoadFromGridView(hairColorGrid);
                l.EyeColors = LoadFromGridView(eyeColorGrid);
                l.Heads = LoadFromGridView(headGrid);
                l.Mouths = LoadFromGridView(mouthGrid);
                l.Eyes = LoadFromGridView(eyeGrid);

                if (SelectedPlayerLook == null)
                {
                    Program.PlayerLooks.Add(l);
                }

                ResetPlayerLookEditor();
            }
        }
    }
}
