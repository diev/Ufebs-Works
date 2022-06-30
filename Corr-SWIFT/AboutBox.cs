#region License
/*
Copyright 2022 Dmitrii Evdokimov
Open source software

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System.Reflection;

namespace CorrSWIFT;

partial class AboutBox : Form
{
    public AboutBox()
    {
        InitializeComponent();

        Text = $"О программе {AssemblyTitle}";

        labelProductName.Text = AssemblyProduct;
        labelVersion.Text = $"Версия {AssemblyVersion}";
        labelCopyright.Text = AssemblyCopyright;
        labelCompanyName.Text = AssemblyCompany;

        textBoxDescription.Text = AssemblyDescription;
    }

    #region Методы доступа к атрибутам сборки

    public static string AssemblyTitle
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

            return attributes.Length > 0 && ((AssemblyTitleAttribute)attributes[0]).Title != string.Empty
                ? ((AssemblyTitleAttribute)attributes[0]).Title
                : Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
        }
    }

    public static string? AssemblyVersion
        => Assembly.GetExecutingAssembly().GetName().Version?.ToString();

    public static string AssemblyDescription
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

            return attributes.Length == 0
                ? string.Empty
                : ((AssemblyDescriptionAttribute)attributes[0]).Description;
        }
    }

    public static string AssemblyProduct
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyProductAttribute), false);

            return attributes.Length == 0
                ? string.Empty
                : ((AssemblyProductAttribute)attributes[0]).Product;
        }
    }

    public static string AssemblyCopyright
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

            return attributes.Length == 0
                ? string.Empty
                : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
        }
    }

    public static string AssemblyCompany
    {
        get
        {
            object[] attributes = Assembly.GetExecutingAssembly()
                .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

            return attributes.Length == 0
                ? string.Empty
                : ((AssemblyCompanyAttribute)attributes[0]).Company;
        }
    }

    #endregion

    private void OkButton_Click(object sender, EventArgs e)
    {
        Close();
    }
}
