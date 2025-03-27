using System.Windows;
using System.Windows.Controls;
using Zoo_WPF.Models;
using Zoo_WPF.Repositories;

namespace Zoo_WPF
{
    public partial class MainWindow : Window
    {
        private readonly ZooRepository _zooRepository;

        public MainWindow()
        {
            InitializeComponent();
            _zooRepository = new ZooRepository("Data Source=NTTD-CGMN3J3;Initial Catalog=ZooDB;Persist Security Info=True;User ID=sa;Password=cR2Aa03e;Encrypt=False");
            LoadZoos();
            LoadAnimals();
        }

        private void LoadZoos()
        {
            ListadeZoos.ItemsSource = _zooRepository.GetZoos();
            ListadeZoos.DisplayMemberPath = "Ubicacion";
            ListadeZoos.SelectedValuePath = "Id";
        }

        private void LoadAnimalsInZoo(int zooId)
        {
            ListadeAnimalesAsociados.ItemsSource = _zooRepository.GetAnimalsInZoo(zooId);
            ListadeAnimalesAsociados.DisplayMemberPath = "Nombre";
            ListadeAnimalesAsociados.SelectedValuePath = "Id";
        }

        private void LoadAnimals()
        {
            ListadeAnimales.ItemsSource = _zooRepository.GetAnimals();
            ListadeAnimales.DisplayMemberPath = "Nombre";
            ListadeAnimales.SelectedValuePath = "Id";
        }

        private void ListadeZoos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListadeZoos.SelectedValue != null)
            {
                int zooId = (int)ListadeZoos.SelectedValue;
                LoadAnimalsInZoo(zooId);
                MuestraZooElegidoEnTextBox(sender, e);
            }
        }

        private void Eliminar_Zoo_Boton_Click(object sender, RoutedEventArgs e)
        {
            if (ListadeZoos.SelectedValue != null)
            {
                int zooId = (int)ListadeZoos.SelectedValue;
                _zooRepository.RemoveZoo(zooId);
                LoadZoos();
                ListadeAnimalesAsociados.ItemsSource = null;
            }
        }

        private void Agregar_Zoo_Boton_Click(object sender, RoutedEventArgs e)
        {
            var zoo = new Zoo { Ubicacion = TextBox.Text };
            _zooRepository.AddZoo(zoo);
            TextBox.Text = "";
            LoadZoos();
        }

        private void AgregarAnimalEnZoo_Boton_Click(object sender, RoutedEventArgs e)
        {
            if (ListadeZoos.SelectedValue != null && ListadeAnimales.SelectedValue != null)
            {
                int zooId = (int)ListadeZoos.SelectedValue;
                int animalId = (int)ListadeAnimales.SelectedValue;
                _zooRepository.AddAnimalToZoo(zooId, animalId);
                LoadAnimalsInZoo(zooId);
            }
        }

        private void Agregar_Animal_Boton_Click(object sender, RoutedEventArgs e)
        {
            var animal = new Animal { Nombre = TextBox.Text };
            _zooRepository.AddAnimal(animal);
            TextBox.Text = "";
            LoadAnimals();
        }

        private void Eliminar_Animal_Boton_Click(object sender, RoutedEventArgs e)
        {
            if (ListadeAnimales.SelectedValue != null)
            {
                int animalId = (int)ListadeAnimales.SelectedValue;
                _zooRepository.RemoveAnimal(animalId);
                LoadAnimals();
                if (ListadeZoos.SelectedValue != null)
                {
                    LoadAnimalsInZoo((int)ListadeZoos.SelectedValue);
                }
            }
        }

        private void Quitar_Animal_Boton_Click(object sender, RoutedEventArgs e)
        {
            if (ListadeZoos.SelectedValue != null && ListadeAnimalesAsociados.SelectedValue != null)
            {
                int zooId = (int)ListadeZoos.SelectedValue;
                int animalId = (int)ListadeAnimalesAsociados.SelectedValue;
                _zooRepository.RemoveAnimalFromZoo(zooId, animalId);
                LoadAnimalsInZoo(zooId);
            }
        }

        private void MuestraZooElegidoEnTextBox(object sender, SelectionChangedEventArgs e)
        {
            if (ListadeZoos.SelectedItem != null)
            {
                var selectedZoo = (Zoo)ListadeZoos.SelectedItem;
                TextBox.Text = selectedZoo.Ubicacion;
            }
        }

        private void MuestraAnimalElegidoEnTextBox(object sender, SelectionChangedEventArgs e)
        {
            if (ListadeAnimales.SelectedItem != null)
            {
                var selectedAnimal = (Animal)ListadeAnimales.SelectedItem;
                TextBox.Text = selectedAnimal.Nombre;
            }
        }

        private void ListadeAnimales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MuestraAnimalElegidoEnTextBox(sender, e);
        }

        private void Actualizar_Zoo_Boton_Click(object sender, RoutedEventArgs e)
        {
            if (ListadeZoos.SelectedValue != null)
            {
                var zoo = new Zoo
                {
                    Id = (int)ListadeZoos.SelectedValue,
                    Ubicacion = TextBox.Text
                };
                _zooRepository.UpdateZoo(zoo);
                LoadZoos();
            }
        }

        private void Actualizar_Animal_Boton_Click(object sender, RoutedEventArgs e)
        {
            if (ListadeAnimales.SelectedValue != null)
            {
                var animal = new Animal
                {
                    Id = (int)ListadeAnimales.SelectedValue,
                    Nombre = TextBox.Text
                };
                _zooRepository.UpdateAnimal(animal);
                LoadAnimals();
                if (ListadeZoos.SelectedValue != null)
                {
                    LoadAnimalsInZoo((int)ListadeZoos.SelectedValue);
                }
            }
        }

        private void ListadeAnimalesAsociados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}