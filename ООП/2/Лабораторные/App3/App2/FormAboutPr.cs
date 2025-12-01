using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App2
{
    public partial class FormAboutPr : Form
    {
        public FormAboutPr()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SortForm sortForm = new SortForm();
            sortForm.Show();
            this.Close();
        }
    }
}

/*
  string pattern = @"^\+375(29|33|44|25)\d{7}$";  
  string pattern = @"^\d{7}$";  
  string pattern = @"^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$";
  string pattern = @"^[A-Za-zА-Яа-яЁё]+$";  
  string pattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";
  ?<=<img .*?src\s*=\s*" — положительная обратная ссылка для поиска вхождения, перед которым есть тег <img>, атрибут src.
  [a-zA-Zа-яА-Я,-;:]{5,50} — строка длиной от 5 до 50 символов, состоящая из букв и некоторых знаков препинания.
  /^[a-z0-9_-]{3,16}$/ — проверка логина (3-16 символов: буквы, цифры, дефис, нижнее подчеркивание).
  /^#?([a-f0-9]{6}|[a-f0-9]{3})$/ — проверка hex-цвета 
  /^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$/ — проверка URL.
  /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][09]?)$/ — проверка IP-адреса.
  /^<([a-z]+)([^<]+)*(?:>(.*)<\/\1>|\s+\/>)$/ — проверка HTML-тега.
 */
