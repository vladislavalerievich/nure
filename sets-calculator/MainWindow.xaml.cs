using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Set {
    /// <summary>
    /// Множество.
    /// </summary>
    /// <typeparam name="T"> Тип данных, хранимых во множестве. </typeparam>
    public class Set<T> : IEnumerable<T> {
        /// <summary>
        /// Коллекция хранимых данных.
        /// </summary>
        private readonly List<T> _items = new List<T> ();

        /// <summary>
        /// Количество элементов.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Добавить данные во множество.
        /// </summary>
        /// <param name="item"> Добавляемые данные. </param>
        public void Add (T item) {
            // Проверяем входные данные на пустоту.
            if (item == null) {
                throw new ArgumentNullException (nameof (item));
            }

            // Множество может содержать только уникальные элементы,
            // поэтому если множество уже содержит такой элемент данных, то не добавляем его.
            if (!_items.Contains (item)) {
                _items.Add (item);
            }
        }

        /// <summary>
        /// Удалить элемент из множества.
        /// </summary>
        /// <param name="item"> Удаляемый элемент данных. </param>
        public void Remove (T item) {
            // Проверяем входные данные на пустоту.
            if (item == null) {
                throw new ArgumentNullException (nameof (item));
            }

            // Если коллекция не содержит данный элемент, то мы не можем его удалить.
            if (!_items.Contains (item)) {
                throw new KeyNotFoundException ($"Элемент {item} не найден в множестве.");
            }

            // Удаляем элемент из коллекции.
            _items.Remove (item);
        }

        /// <summary>
        /// Объединение множеств.
        /// </summary>
        /// <param name="set1"> Первое множество. </param>
        /// <param name="set2"> Второе множество. </param>
        /// <returns> Новое множество, содержащее все уникальные элементы полученных множеств. </returns>
        public static Set<T> Union (Set<T> set1, Set<T> set2) {
            // Проверяем входные данные на пустоту.
            if (set1 == null) {
                throw new ArgumentNullException (nameof (set1));
            }

            if (set2 == null) {
                throw new ArgumentNullException (nameof (set2));
            }

            // Результирующее множество.
            Set<T> resultSet = new Set<T> ();

            // Элементы данных результирующего множества.
            List<T> items = new List<T> ();

            // Если первое входное множество содержит элементы данных,
            // то добавляем их в результирующее множество.
            if (set1._items != null && set1._items.Count > 0) {
                // т.к. список является ссылочным типом, 
                // то необходимо не просто передавать данные, а создавать их дубликаты.
                items.AddRange (new List<T> (set1._items));
            }

            // Если второе входное множество содержит элементы данных, 
            // то добавляем из в результирующее множество.
            if (set2._items != null && set2._items.Count > 0) {
                // т.к. список является ссылочным типом, 
                // то необходимо не просто передавать данные, а создавать их дубликаты.
                items.AddRange (new List<T> (set2._items));
            }

            // Удаляем все дубликаты из результирующего множества элементов данных.
            resultSet._items = items.Distinct ().ToList ();

            // Возвращаем результирующее множество.
            return resultSet;
        }

        /// <summary>
        /// Пересечение множеств.
        /// </summary>
        /// <param name="set1"> Первое множество. </param>
        /// <param name="set2"> Второе множество. </param>
        /// <returns> Новое множество, содержащее совпадающие элементы данных из полученных множеств. </returns>
        public static Set<T> Intersection (Set<T> set1, Set<T> set2) {
            // Проверяем входные данные на пустоту.
            if (set1 == null) {
                throw new ArgumentNullException (nameof (set1));
            }

            if (set2 == null) {
                throw new ArgumentNullException (nameof (set2));
            }

            // Результирующее множество.
            Set<T> resultSet = new Set<T> ();

            // Выбираем множество содержащее наименьшее количество элементов.
            if (set1.Count < set2.Count) {
                // Первое множество меньше.
                // Проверяем все элементы выбранного множества.
                foreach (T item in set1._items) {
                    // Если элемент из первого множества содержится во втором множестве,
                    // то добавляем его в результирующее множество.
                    if (set2._items.Contains (item)) {
                        resultSet.Add (item);
                    }
                }
            } else {
                // Второе множество меньше или множества равны.
                // Проверяем все элементы выбранного множества.
                foreach (T item in set2._items) {
                    // Если элемент из второго множества содержится в первом множестве,
                    // то добавляем его в результирующее множество.
                    if (set1._items.Contains (item)) {
                        resultSet.Add (item);
                    }
                }
            }

            // Возвращаем результирующее множество.
            return resultSet;
        }

        /// <summary>
        /// Разность множеств.
        /// </summary>
        /// <param name="set1"> Первое множество. </param>
        /// <param name="set2"> Второе множество. </param>
        /// <returns> Новое множество, содержащее не совпадающие элементы данных между полученными множествами. </returns>
        public static Set<T> Difference (Set<T> set1, Set<T> set2) {
            // Проверяем входные данные на пустоту.
            if (set1 == null) {
                throw new ArgumentNullException (nameof (set1));
            }

            if (set2 == null) {
                throw new ArgumentNullException (nameof (set2));
            }

            // Результирующее множество.
            Set<T> resultSet = new Set<T> ();

            // Проходим по всем элементам первого множества.
            foreach (T item in set1._items) {
                // Если элемент из первого множества не содержится во втором множестве,
                // то добавляем его в результирующее множество.
                if (!set2._items.Contains (item)) {
                    resultSet.Add (item);
                }
            }

            // Удаляем все дубликаты из результирующего множества элементов данных.
            resultSet._items = resultSet._items.Distinct ().ToList ();

            // Возвращаем результирующее множество.
            return resultSet;
        }

        /// <summary>
        /// Подмножество.
        /// </summary>
        /// <param name="set1"> Множество, проверяемое на вхождение в другое множество. </param>
        /// <param name="set2"> Множество в которое проверяется вхождение другого множества. </param>
        /// <returns> Является ли первое множество подмножеством второго. true - является, false - не является. </returns>
        public static bool Subset (Set<T> set1, Set<T> set2) {
            // Проверяем входные данные на пустоту.
            if (set1 == null) {
                throw new ArgumentNullException (nameof (set1));
            }

            if (set2 == null) {
                throw new ArgumentNullException (nameof (set2));
            }

            // Перебираем элементы первого множества.
            // Если все элементы первого множества содержатся во втором,
            // то это подмножество. Возвращаем истину, иначе ложь.
            bool result = set1._items.All (s => set2._items.Contains (s));
            return result;
        }

        /// <summary>
        /// Вернуть перечислитель, выполняющий перебор всех элементов множества.
        /// </summary>
        /// <returns> Перечислитель, который можно использовать для итерации по коллекции. </returns>
        public IEnumerator<T> GetEnumerator () {
            // Используем перечислитель списка элементов данных множества.
            return _items.GetEnumerator ();
        }

        /// <summary>
        /// Вернуть перечислитель, который осуществляет итерационный переход по множеству.
        /// </summary>
        /// <returns> Объект IEnumerator, который используется для прохода по коллекции. </returns>
        IEnumerator IEnumerable.GetEnumerator () {
            // Используем перечислитель списка элементов данных множества.
            return _items.GetEnumerator ();
        }
    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow () {
            InitializeComponent ();
        }

        private readonly Set<int> set1 = new Set<int> ();
        private readonly Set<int> set2 = new Set<int> ();

        private void InputSetA_PreviewTextInput (object sender, System.Windows.Input.TextCompositionEventArgs e) {
            ProcessInput (e, set1);
        }

        private void InputSetB_PreviewTextInput (object sender, System.Windows.Input.TextCompositionEventArgs e) {
            ProcessInput (e, set2);
        }

        private void ProcessInput (System.Windows.Input.TextCompositionEventArgs e, Set<int> set) {
            int newInput;
            try {
                newInput = int.Parse (e.Text);
            } catch (Exception ex) {
                MessageBox.Show (ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
                return;
            }
            if (set.Contains (newInput)) {
                e.Handled = true;
            } else {
                set.Add (newInput);
            }
        }

        private void SubmitButton_Click (object sender, RoutedEventArgs e) {
            try {
                // check if one or both of inputs are empty
                if (inputSetA.Text.Trim () == string.Empty || inputSetA.Text.Trim () == string.Empty) {
                    throw new FormatException ("Both inputs should have a value");
                }
                // check if any of RadioButtons were chosen 
                if (!(radioButtonStackPanel.Children.OfType<RadioButton> ().Any (rb => rb.IsChecked == true))) {
                    throw new ArgumentNullException ("You have to select an option");
                }

            } catch (Exception ex) {
                MessageBox.Show (ex.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            CalculateResult ();
        }

        public void CalculateResult () {
            string msg;

            dynamic result = new Set<int> () { };

            if (radioBtnUnion.IsChecked == true) {
                result = Set<int>.Union (set1, set2);
            } else if (radioBtnDifference.IsChecked == true) {
                result = Set<int>.Difference (set1, set2);
            } else if (radioBtnIntersection.IsChecked == true) {
                result = Set<int>.Intersection (set1, set2);
            } else if (radioBtnSubset.IsChecked == true) {
                result = Set<int>.Subset (set1, set2);
            }

            msg = string.Join (" ", result);
            MessageBox.Show (msg);
        }
    }
}