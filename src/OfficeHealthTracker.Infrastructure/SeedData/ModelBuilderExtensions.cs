using System;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using OfficeHealthTracker.Domain.Model;
using Microsoft.EntityFrameworkCore;


namespace UI_Layer.Data.SeedData
{
    public class ModelBuilderExtensions
    {
    
        public static Guid fieldType_DropDownId = Guid.Parse("3D986C86-321F-427C-8624-1B8F513C4203");
        public static Guid fieldType_TextBoxId = Guid.Parse("C649547C-70BE-49FB-883B-9189433B7D65");
        public static Guid fieldType_CheckBoxId = Guid.Parse("480874CA-C054-4D1A-9832-74ADA72CD4F6");
        public static Guid fieldType_RadioId = Guid.Parse("29460C45-7226-4FEA-BE84-28A74920FF3B");

        public static Guid userId = Guid.Parse("55402D34-0234-4FA8-B564-FC96641B23D0");

        public static void SeedFieldType(ModelBuilder builder)
        {
            // Seed DropDown FieldType
            FieldType dropDownFieldType = new FieldType
            {
                FieldTypeId = fieldType_DropDownId,
                TypeName = "DropDown"
            };
            builder.Entity<FieldType>().HasData(dropDownFieldType);

            // Seed TextBox FieldType
            FieldType textBoxFieldType = new FieldType
            {
                FieldTypeId = fieldType_TextBoxId,
                TypeName = "TextBox"
            };
            builder.Entity<FieldType>().HasData(textBoxFieldType);

            // Seed CheckBox FieldType
            FieldType checkBoxFieldType = new FieldType
            {
                FieldTypeId = fieldType_CheckBoxId,
                TypeName = "CheckBox"
            };
            builder.Entity<FieldType>().HasData(checkBoxFieldType);

            // Seed Radio FieldType
            FieldType radioFieldType = new FieldType
            {
                FieldTypeId = fieldType_RadioId,
                TypeName = "Radio"
            };
            builder.Entity<FieldType>().HasData(radioFieldType);
        }
        public static void SeedUser(ModelBuilder builder)
        {
         
            User user = new User
            {
                Id = userId,
                Username = "HTT",
                Password = "STPL"
            };
            builder.Entity<User>().HasData(user);
        }
    }
}
