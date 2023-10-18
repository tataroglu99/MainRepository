using DataAccess;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MainRepository
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadVeriler();
        }

        public void loadVeriler()
        {
            try
            {
                CategoryRepository repository = new CategoryRepository();
                gridData.DataSource = repository.All(true);
                gridData.DataBind();
            }

            catch (Exception)
            {

            }
        }

        protected void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                CategoryRepository repository = new CategoryRepository();

                //Category tempItem = new Category();
                //tempItem.CreatedByUserId = 1;
                //tempItem.CreatedOnDate = DateTime.Now;
                //tempItem.Image = "Resim Url";
                //tempItem.IsDeleted = false;
                //tempItem.LastUpdatedOnDate = DateTime.Now;
                //tempItem.LasUpdatedByUserId = "1";
                //tempItem.Name = "Elektronik";

                //Category tempItemm = new Category();
                //tempItemm.CreatedByUserId = 1;
                //tempItemm.CreatedOnDate = DateTime.Now;
                //tempItemm.Image = "Resim Url";
                //tempItemm.IsDeleted = false;
                //tempItemm.LastUpdatedOnDate = DateTime.Now;
                //tempItemm.LasUpdatedByUserId = "1";
                //tempItemm.Name = "Elektrik";

                //List<Category> liste = new List<Category>();
                //liste.Add(tempItem);
                //liste.Add(tempItemm);

                //repository.InsertAll(liste);

                //repository.Commit();

                //int sayi = repository.Count(x => x.Name.Equals("Elektrik"));

                Category tempCat = repository.GetById(23);
                tempCat.Name = "Yeni Kategoridir";
                repository.Update(tempCat);
                repository.Commit();

                string tempObjStr = "";
                repository.GetLogValue(tempCat, ref tempObjStr);

                repository.SoftDelete(tempCat, "IsDeleted", true);
                //repository.Commit();
                GetPropertiesNameOfClass(tempCat);
                loadVeriler();
            }

            catch (Exception)
            {

            }
        }

        public List<string> GetPropertiesNameOfClass(object pObject)
        {
            List<string> propertyList = new List<string>();

            if (pObject != null)
            {
                List<PropertyInfo> properties = pObject.GetType().GetProperties().ToList<PropertyInfo>();

                foreach (var prop in properties)
                {
                    if (!prop.PropertyType.IsClass)
                    {
                        propertyList.Add(prop.Name + ": " + prop.GetValue(pObject, null));
                    }
                    
                }
            }

            return propertyList;
        }
    }
}