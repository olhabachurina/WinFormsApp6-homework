namespace WinFormsApp11
{
    public partial class Form1 : Form
    {
        private Label lengthLabel;
        private TextBox lengthTextBox;
        private CheckBox includeDigitsCheckBox;
        private CheckBox includeLowercaseCheckBox;
        private CheckBox includeUppercaseCheckBox;
        private CheckBox includeSpecialCharsCheckBox;
        private CheckBox excludeSimilarCharsCheckBox;
        private Button generateButton;
        private Label generatedPasswordLabel;
        private RadioButton standardCharacterSetRadioButton;
        private RadioButton customCharacterSetRadioButton;
        private TextBox customCharacterSetTextBox;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Проверяем, что нажата комбинация клавиш Ctrl + C
            if (e.Control && e.KeyCode == Keys.C)
            {
                // Копируем текст из generatedPasswordLabel в буфер обмена
                Clipboard.SetText(generatedPasswordLabel.Text);
            }
        }

        private void InitializeComponent()
        {
            // Настройка основной формы
            this.Text = "Password generator";
            this.Size = new System.Drawing.Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Создаем радиокнопки для выбора набора символов
            standardCharacterSetRadioButton = new RadioButton();
            standardCharacterSetRadioButton.Text = "Стандартный набор символов";
            standardCharacterSetRadioButton.Location = new System.Drawing.Point(20, 260);
            standardCharacterSetRadioButton.CheckedChanged += (s, e) => OnCharacterSetRadioButtonCheckedChanged();

            customCharacterSetRadioButton = new RadioButton();
            customCharacterSetRadioButton.Text = "Собственный набор символов";
            customCharacterSetRadioButton.Location = new System.Drawing.Point(20, 290);
            customCharacterSetRadioButton.CheckedChanged += (s, e) => OnCharacterSetRadioButtonCheckedChanged();

            // Добавление элементов управления на форму
            this.Controls.Add(standardCharacterSetRadioButton);
            this.Controls.Add(customCharacterSetRadioButton);

            lengthLabel = new Label();
            lengthLabel.Text = "Длина пароля:";
            lengthLabel.Location = new System.Drawing.Point(20, 20);

            lengthTextBox = new TextBox();
            lengthTextBox.Location = new System.Drawing.Point(140, 20);

            includeDigitsCheckBox = new CheckBox();
            includeDigitsCheckBox.Text = "Включать цифры";
            includeDigitsCheckBox.Location = new System.Drawing.Point(20, 50);
            includeDigitsCheckBox.AutoSize = true;

            includeLowercaseCheckBox = new CheckBox();
            includeLowercaseCheckBox.Text = "Включать маленькие буквы";
            includeLowercaseCheckBox.Location = new System.Drawing.Point(20, 80);
            includeLowercaseCheckBox.AutoSize = true;

            includeUppercaseCheckBox = new CheckBox();
            includeUppercaseCheckBox.Text = "Включать заглавные буквы";
            includeUppercaseCheckBox.Location = new System.Drawing.Point(20, 110);
            includeUppercaseCheckBox.AutoSize = true;

            includeSpecialCharsCheckBox = new CheckBox();
            includeSpecialCharsCheckBox.Text = "Включать специальные символы";
            includeSpecialCharsCheckBox.Location = new System.Drawing.Point(20, 140);
            includeSpecialCharsCheckBox.AutoSize = true;

            excludeSimilarCharsCheckBox = new CheckBox();
            excludeSimilarCharsCheckBox.Text = "Исключать похожие символы";
            excludeSimilarCharsCheckBox.Location = new System.Drawing.Point(20, 170);
            excludeSimilarCharsCheckBox.AutoSize = true;

            generateButton = new Button();
            generateButton.Text = "Генерировать пароль";
            generateButton.Location = new System.Drawing.Point(20, 200);
            generateButton.Click += generateButton_Click;
            generateButton.AutoSize = true;

            generatedPasswordLabel = new Label();
            generatedPasswordLabel.Text = "";
            generatedPasswordLabel.Location = new System.Drawing.Point(20, 230);

            // Добавление элементов на форму
            this.Controls.Add(lengthLabel);
            this.Controls.Add(lengthTextBox);
            this.Controls.Add(includeDigitsCheckBox);
            this.Controls.Add(includeLowercaseCheckBox);
            this.Controls.Add(includeUppercaseCheckBox);
            this.Controls.Add(includeSpecialCharsCheckBox);
            this.Controls.Add(excludeSimilarCharsCheckBox);
            this.Controls.Add(generateButton);
            this.Controls.Add(generatedPasswordLabel);
        }

        private void OnCharacterSetRadioButtonCheckedChanged()
        {
            if (standardCharacterSetRadioButton.Checked)
            {
                // Включите элементы управления для стандартного набора символов
                // Отключите элементы управления для собственного набора символов
            }
            else if (customCharacterSetRadioButton.Checked)
            {
                // Включите элементы управления для собственного набора символов
                // Отключите элементы управления для стандартного набора символов
            }
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            int length;
            if (!int.TryParse(lengthTextBox.Text, out length) || length < 4 || length > 32)
            {
                MessageBox.Show("Введите корректную длину пароля от 4 до 32 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string customCharacterSet = string.Empty;
            if (customCharacterSetRadioButton.Checked && customCharacterSetTextBox != null)
            {
                customCharacterSet = customCharacterSetTextBox.Text;
            }

            bool includeDigits = includeDigitsCheckBox.Checked;
            bool includeLowercase = includeLowercaseCheckBox.Checked;
            bool includeUppercase = includeUppercaseCheckBox.Checked;
            bool includeSpecialChars = includeSpecialCharsCheckBox.Checked;
            bool excludeSimilarChars = excludeSimilarCharsCheckBox.Checked;

            string password = GeneratePassword(length, includeDigits, includeLowercase, includeUppercase, includeSpecialChars, excludeSimilarChars, customCharacterSet);

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Выберите хотя бы один набор символов для включения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                generatedPasswordLabel.Text = password;
                generatedPasswordLabel.ForeColor = System.Drawing.Color.Red;
                generatedPasswordLabel.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            }
        }

        private string GeneratePassword(int length, bool includeDigits, bool includeLowercase, bool includeUppercase, bool includeSpecialChars, bool excludeSimilarChars, string customCharacterSet)
        {
            string characters = "";

            if (includeDigits)
            {
                characters += "0123456789";
            }
            if (includeLowercase)
            {
                characters += "abcdefghijklmnopqrstuvwxyz";
            }
            if (includeUppercase)
            {
                characters += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            if (includeSpecialChars)
            {
                characters += "!@#$%^&*()_+=-[]{}|;:,.<>?";
            }

            if (!string.IsNullOrEmpty(customCharacterSet))
            {
                characters += customCharacterSet;
            }

            if (excludeSimilarChars)
            {
                characters = new string(characters.Where(c => "il1Lo0O".IndexOf(c) == -1).ToArray());
            }

            if (string.IsNullOrEmpty(characters))
            {
                return string.Empty;
            }

            Random random = new Random();
            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = characters[random.Next(characters.Length)];
            }

            return new string(password);
        }
    }
}