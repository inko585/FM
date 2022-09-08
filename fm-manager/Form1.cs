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
            ethnieDeleteButton.Enabled = false;
            nationDeleteButton.Enabled = false;
            associationDeleteBtn.Enabled = false;
            ResetEditor();

        }

        private BindingSource nationBindingSource = new BindingSource();
        private BindingSource subnationBindingSource = new BindingSource();
        private BindingSource ethnieBindingSource = new BindingSource();
        private BindingSource subethnieBindingSource = new BindingSource();
        private BindingSource associationBindingSource = new BindingSource();

        private void ResetEditor()
        {
            nationBindingSource.DataSource = Program.Nations;
            ethnieBindingSource.DataSource = Program.Ethnies;
            nationSelection.DataSource = nationBindingSource.DataSource;
            ethnieSelection.DataSource = ethnieBindingSource.DataSource;
            associationSelection.DataSource = associationBindingSource.DataSource;
            mainEthnieSelection.DataSource = ethnieBindingSource.DataSource;
            nationSelection.DisplayMember = "Name";
            nationSelection.ValueMember = "Name";
            ethnieSelection.DisplayMember = "Name";
            ethnieSelection.ValueMember = "Name";
            associationSelection.DisplayMember = "Name";
            associationSelection.ValueMember = "Name";
            mainEthnieSelection.ValueMember = "Name";
            mainEthnieSelection.DisplayMember = "Name";
            ResetNationGrids();
            ResetEthnieGrids();
            ResetAssociationGrids();
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



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Export();
            MessageBox.Show("Datensatz gespeichert!");
        }

        private Nation SelectedNation = null;
        private Ethnie SelectedEthnie = null;
        private Association SelectedAssociation = null;


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
                c.Sponsors = LoadFromGridView(sponsorGrid);
                c.Name = nationTextBox.Text;
                c.SubNations = LoadFromGridView(subnationGrid);
                c.MainEthnie = (Ethnie)mainEthnieSelection.SelectedItem;
                c.SubEthnies = LoadFromEthnieGridView(ethnieGrid).Where(subet => !subet.Text.Equals(c.MainEthnie.Name)).ToList();
                c.Short = nationShortTextBox.Text;

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
            SetUpOccurrenceGridView(sponsorGrid, "Sponsor", "Größe", new List<Occurrence>());
            SetUpDropDownOccurrenceGridView(subnationGrid, "Ausländer", "Häufigkeit", typeof(Nation), subnationBindingSource, new List<Occurrence>());
        }

        private void ResetEthnieGrids()
        {
            SetUpOccurrenceGridView(firstNameGrid, "Vorname", "Häufigkeit", new List<Occurrence>());
            SetUpOccurrenceGridView(lastNameGrid, "Nachname", "Häufigkeit", new List<Occurrence>());
        }

        private void ResetAssociationGrids()
        {
            SetUpDropDownOccurrenceGridView(nationGrid, "Nation", "Häufigkeit", typeof(Nation), nationBindingSource, new List<Occurrence>());            
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
                    if (et != n.MainEthnie)
                    {
                        bList2.Add(et);
                    }
                }
                mainEthnieSelection.SelectedItem = n.MainEthnie;
                SetUpEthnieOccurrenceGridView(ethnieGrid, n.SubEthnies);
                SetUpOccurrenceGridView(cityGrid, "Stadt", "Häufigkeit", n.Cities);
                SetUpOccurrenceGridView(prefix1Grid, "Vereins Präfix I", "Häufigkeit", n.FirstPrefixes);
                SetUpOccurrenceGridView(prefix2Grid, "Vereins Präfix II", "Häufigkeit", n.SecondPrefixes);
                SetUpOccurrenceGridView(suffixGrid, "Vereins Suffix", "Häufigkeit", n.Suffixes);
                SetUpOccurrenceGridView(sponsorGrid, "Sponsor", "Größe", n.Sponsors);
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
                associatonLevel.Value = a.Power;
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
            } else
            {
                if (associatonLevel.Value < 1 || associatonLevel.Value > 10)
                {
                    MessageBox.Show("Der Verband braucht ein Level > 0");
                } else
                {
                    if (associatonDepth.Value < 1 || associatonLevel.Value > 10)
                    {
                        MessageBox.Show("Der Verband braucht eine Ligen Zahl > 0");
                    } else
                    {
                        a.Name = associationTextBox.Text;
                        a.Power = (int)associatonLevel.Value;
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
            ResetAssociationEditor();
            MessageBox.Show("Datensatz importiert!");
        }


    }
}
