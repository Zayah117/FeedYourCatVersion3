using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Feed_your_cat_
{
    class Cat
    {
        Random random = new Random();
        private string name;
        private int hunger = 100;
        private bool alive = true;
        public string Status = "normal";
        public bool DeclaredDead = false;
        public Label MyLabel;
        public PictureBox MyPictureBox;
        public System.Drawing.Image Starved;
        public System.Drawing.Image Hungry;
        public System.Drawing.Image Normal;
        public System.Drawing.Image Full;
        public System.Drawing.Image Boom;

        public void UpdateLabels()
        {
            MyLabel.Text = "Hunger: " + hunger + "%";
            updatePicture();
        }
        public void GiveName(string newName)
        {
            name = newName;
        }
        private void updatePicture()
        {
            if (Status == "normal")
            {
                MyPictureBox.Image = Normal;
            }
            if (Status == "starved")
            {
                MyPictureBox.Image = Starved;
            }
            if (Status == "hungry")
            {
                MyPictureBox.Image = Hungry;
            }
            if (Status == "full")
            {
                MyPictureBox.Image = Full;
            }
            if (Status == "boom")
            {
                MyPictureBox.Image = Boom;
            }
        }

        public void GetHungry(int ammount)
        {
            hunger -= ammount;
        }
        public void FeedCat(int ammount)
        {
            if (alive == true)
            {
                hunger += ammount;
            }
        }

        public void UpdateStatus()
        {
            if (hunger <= 0)
            {
                Status = "starved";
            }
            if (hunger > 0 && hunger <= 50)
            {
                Status = "hungry";
            }
            if (hunger > 50 && hunger < 150)
            {
                Status = "normal";
            }
            if (hunger >= 150 && hunger <= 200)
            {
                Status = "full";
            }
            if (hunger > 200)
            {
                Status = "boom";
            }
        }
        public bool IsAlive()
        {
            if (Status == "starved" || Status == "boom")
            {
                alive = false;
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
