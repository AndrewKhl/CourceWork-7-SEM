using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHotel.Core
{
    public class LivingRoom : Room
    {
        public bool Status { get; set; }

        public int Cost { get; set; }

        public string ServicesStr { get; set; }

        public List<string> Services { get; set; }

        public string PhotoStr { get; set; }

        public List<string> Photo { get; set; }

        public void AddService(int id)
        {
            SetServiceCollection();

            if (!Services.Contains(id.ToString()))
            {
                Services.Add(id.ToString());
                ServicesStr = StringHelper.AddIdItem(ServicesStr, id);
            }
        }

        public bool AddPhoto(string path)
        {
            SetPhotoCollection();

            if (!Photo.Contains(path))
            {
                Photo.Add(path);
                PhotoStr = StringHelper.AddIdItem(PhotoStr, path);
                return true;
            }

            return false;
        }

        public bool RemovePhoto(string path)
        {
            SetPhotoCollection();

            if (Photo.Contains(path))
            {
                Photo.Remove(path);
                PhotoStr = StringHelper.JoinString(Photo);
                return true;
            }

            return false;
        }

        public void UpdateCollections()
        {
            SetPhotoCollection();
            SetServiceCollection();
        }

        private void SetPhotoCollection()
        {
            if (Photo == null)
                Photo = StringHelper.GetIdList(PhotoStr) ?? new List<string>();
        }

        private void SetServiceCollection()
        {
            if (Services == null)
                Services = StringHelper.GetIdList(ServicesStr) ?? new List<string>();
        }
    }
}
