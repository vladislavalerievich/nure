using System;
using static System.Console;



namespace LinkedList
{
    class Node
    {
        internal int Data { get; set; }
        internal Node Next { get; set; }

        public Node(int d)
        {
            Data = d;
            Next = null;
        }
    }

    
    class SinglyLinkedList
    {
        //----------------------------------
        //           PROPERTIES
        //----------------------------------

        internal Node Head;  
        internal int Length
        {
            get
            {
                int length = 0;
                Node current = Head;
                while(true) // traversing until the end of the list
                {
                    if (current is Node)
                    {
                        length++;
                        current = current.Next; 
                    }
                    else
                    {
                        return length; 
                    }
                }
            }
        }
      

        //----------------------------------
        //         USEFUL METHODS 
        //----------------------------------

        public void Print()
        {
            Node current = Head;
            while(current != null)
            {
                Write(current.Data + " ");
                current = current.Next;
            }
            WriteLine(); // Just write space. Carriage return
        }

        private void AddLast(int data)
        {
            Node current = Head;
            while (current.Next != null)
            {
                current = current.Next;
            }

            Node newNode = new Node(data);
            current.Next = newNode;
        }

        private Node GetNode(int index)
        {
            if (index >= this.Length || index < 0)
            {
                throw new IndexOutOfRangeException("List index out of range");
            }

            Node currNode = Head;

            for (int i = 0; i < index; i++)
            {
                currNode = currNode.Next;
            }
            return currNode;
        }
     
        public void Filter(Func<int, bool> f)
        {
            int i = 0;
            while (i != Length)
            {
                if (f(GetNode(i).Data))
                {
                    Remove(i);
                }
                else
                {
                    i++;
                }
            }
        }

        public void Remove(int index)
        {
            if (index >= this.Length || index < 0)
            {
                throw new IndexOutOfRangeException("List index out of range");
            }

            if (index == 0)
            {
                if (Length == 1)
                {
                    Head = null;
                    return;
                }
                Head = Head.Next;
                return;
            }

            Node prevNode = GetNode(index - 1);
            prevNode.Next = prevNode.Next.Next;
        }


        //----------------------------------
        //              TASKS 
        //----------------------------------


        // 1) Конструктор з одним параметром(число);
        public SinglyLinkedList(int data)
        {
            Head = new Node(data);           
        }


        // 2) Конструктор з двома параметрами(число, посилання на наступний елемент)
        public SinglyLinkedList(int data, Node next)
        {
            Head = new Node(data);
            Head.Next = next;
        }


        // 4) Метод додавання нового елементу першим у список;
        public void AddFirst(int data)
        {
            Node newNode = new Node(data);
            newNode.Next = Head;
            Head = newNode;

        }    


        // 7) Не рекурсивний метод додавання нового елемента n-ним у список;
        public void AddAfterIndex(int index, int data)
        {
            if (index > this.Length || index < 0)
            {
                throw new IndexOutOfRangeException("List index out of range");
            }
          
            if (index == 0) // first
            {
                AddFirst(data);    
            }
            else if (index == Length) // last
            {
                AddLast(data);
            }
            else // in the middle
            {
                Node prevNode = GetNode(index - 1);
                Node newNode = new Node(data);
                newNode.Next = prevNode.Next;
                prevNode.Next = newNode;
            }

        }


        // 19) Не рекурсивний метод видалення всіх елементів із заданим значенням;
        public void DeleteByKey(int key)
        {
            Filter((i) => i == key);
        }

        //22) Метод видалення всіх елементів з від'ємними значеннями;
        public void DeleteNegative()
        {
            Filter((i) => i < 0);
        }
        

        //28) Не рекурсивний метод друку елементів списку у зворотному порядку у рядок;      

        // NON RECURSIVELY print reverse of a Linked List WITHOUT actually REVERSING
        // We can use other data strucure like array, stack or even create another linked list
        // Though it's inneficient at all
        // Or we can just use a convenient method that works as an indexer, luckily we have one :)
        public void PrintReverse() 
        {
            for (int i = Length; i > 0; i--) 
            {
                Node node = GetNode(i - 1);
                Write(node.Data + " ");
            }
            WriteLine();
        }
        

        //38) Метод сортування елементів списку за зменшенням числових значень;
        // The most basic sorting algrorithm 
        public void Sort()
        {
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < Length - 1; j++)
                {
                    Node a = GetNode(j);
                    Node b = GetNode(j + 1);

                    if (a.Data < b.Data)
                    {
                        Swap(a, b);
                    }
                }
            }
        }
        private void Swap(Node a, Node b)
        {
            int t = a.Data;
            a.Data = b.Data;
            b.Data = t;
        }

                          
        //53) Індексатор з одним параметром, який дозволяє за значенням елемента знайти його порядковий номер у списку;
        public int this[int num]
        {
            get
            {                
                Node currentNode = Head;

                for (int i = 0; i < Length; i++)
                {                    
                    if (currentNode.Data == num)
                    {
                        return i; 
                    }

                    currentNode = currentNode.Next;
                }
                return -1; // Not found
            }
        }

    }





    //---------------------------------------------------------------------------------------------------------------------------
    class Program
    {
        static void Main()
        {
            //-------------------------------------------------------------------------------------------------------------------
            //                                     Must implement the following functionality
            //-------------------------------------------------------------------------------------------------------------------
            //1.        Constructor with one parameter(number);
            //2.        Constructor with two parameters(number, reference to the next item);
            //4.        Method of adding a new element to be the first in the list;
            //7.        Not a recursive method of adding a new element to the list of n-them;
            //19.       Not a recursive method of removing all elements with a given value;
            //22.       Method of removing all elements with negative values;
            //28.       Not a recursive method of printing list items in reverse order in a string;
            //38.       Method of sorting list items by decreasing numeric values;
            //53.       Indexer with one parameter, which allows to find the serial number in the list by the value of the element;
            //-------------------------------------------------------------------------------------------------------------------

            //-------------------------------------------------------------------------------------------------------------------
            //                                 Реалізується  наступні функціональні можливості
            //-------------------------------------------------------------------------------------------------------------------
            //1.        Конструктор з одним параметром(число); 
            //2.        Конструктор з двома параметрами(число, посилання на наступний елемент);
            //4.        Метод додавання нового елементу першим у список;
            //7.        Не рекурсивний метод додавання нового елемента n-ним у список;
            //19.		Не рекурсивний метод видалення всіх елементів із заданим значенням;
            //22.		Метод видалення всіх елементів з від'ємними значеннями;
            //28.		Не рекурсивний метод друку елементів списку у зворотному порядку у рядок;
            //38.		Метод сортування елементів списку за зменшенням числових значень;
            //53.		Індексатор з одним параметром, який дозволяє за значенням елемента знайти його порядковий номер у списку;
            //-------------------------------------------------------------------------------------------------------------------

            WriteLine("1)Конструктор з одним параметром (число)");
            SinglyLinkedList myList = new SinglyLinkedList(-1);
            Write("List: ");
            myList.Print();
            WriteLine("----------------------------------------------------------------------------------");


            WriteLine("2) Конструктор з двома параметрами(число, посилання на наступний елемент);");
            Node n = new Node(-1);
            SinglyLinkedList mySecondList = new SinglyLinkedList(-2, n);
            Write("List: ");
            mySecondList.Print();
            WriteLine("----------------------------------------------------------------------------------");


            WriteLine("4) Метод додавання нового елементу першим у список;");
            for (int i = 0; i < 11; i++)
            {
                myList.AddFirst(i);
            }
            Write("List: ");
            myList.Print();
            WriteLine("----------------------------------------------------------------------------------");


            WriteLine("7) Не рекурсивний метод додавання нового елемента n - ним у список;");
            Write("List before:   ");
            myList.Print();

            int index = 0;
            int insert = 777;
            
            myList.AddAfterIndex(index, insert);
            WriteLine("Inserted: " + insert + " at position: " + index);

            Write("List after:   ");
            myList.Print();
            WriteLine("----------------------------------------------------------------------------------");
            

            WriteLine("19) Не рекурсивний метод видалення всіх елементів із заданим значенням;");
            for (int i = 0; i < 10; i++)
            {
                myList.AddFirst(6);
            }
            Write("Before: ");
            myList.Print();
            myList.DeleteByKey(6);
            Write("After: ");
            myList.Print();
            WriteLine("----------------------------------------------------------------------------------");
            

            WriteLine("22) Метод видалення всіх елементів з від'ємними значеннями;");
            myList.AddFirst(-123);
            myList.AddFirst(-13);
            Write("Before: ");
            myList.Print();
            myList.DeleteNegative();
            Write("After: ");
            myList.Print();
            WriteLine("----------------------------------------------------------------------------------");
            

            WriteLine("28) Не рекурсивний метод друку елементів списку у зворотному порядку у рядок;");
            Write("Before: ");
            myList.Print();
            Write("After: ");
            myList.PrintReverse();
            Write("List hasn't changed: ");
            myList.Print();
            WriteLine("----------------------------------------------------------------------------------");
            

            WriteLine("38) Метод сортування елементів списку за зменшенням числових значень;");
            Write("Before: ");
            myList.Print();
            myList.Sort();
            Write("After: ");
            myList.Print();
            WriteLine("----------------------------------------------------------------------------------");


            WriteLine("53) Індексатор з одним параметром, який дозволяє за значенням елемента знайти його порядковий номер у списку;");
            Write("List: ");
            myList.Print();
            int index1 = myList[77];
            Write("Значення елемента: 77, його порядковий номер у списку: " + index1 +"\n");
            int index2 = myList[1];
            Write("Значення елемента: 1, його порядковий номер у списку: " + index2 + "\n");
            WriteLine("----------------------------------------------------------------------------------");

            ReadLine();
        }
    }
}



