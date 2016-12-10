using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProCP.Classes
{
    class Images
    {
        private string CropName;
        private List<Image> images;
        public Images(string cropname) {
            this.CropName = cropname;
            images = new List<Image>();
            
        }
        public Images(string cropname, Image image1,Image image2, Image image3, Image image4)
        {
            this.CropName = cropname;
            images = new List<Image>();
            setImage(image1);
            setImage(image2);
            setImage(image3);
            setImage(image4);

        }

        public Image GetImage(int image) {
            if(images.Count >= image)
            {
                return images[image];
            }
            else
            {
                return null;
            }
        }
        public void setImage(Image image)
        {
            images.Add(image);
        }

        
    }
}
