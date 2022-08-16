namespace Telegram.Languages
{
    public static class Arabic
    {
        public const string ChooseOrAdd = "اختر من القائمة أو أدخل القيمة";
        public const string AddBrand = "أدخل اسم المُصنع";
        public const string AddModel = "أدخل الموديل";
        public const string AddColor = "أدخل اللون";
        public const string AddNumber = "أدخل رقم السيارة";
        public const string EntreValidOption = "أدخل اختيار صائب";
        public const string UnkownError = "حدث خطأ، يرى التواصل مع المسؤول";

        public const string AdminUsage = "قم بالاختيار بين:\n" +
                                         "/add      - إضافة سيارة\n" +
                                         "/remove   - حذف سيارة\n" +
                                         "/list     - عرض السيارات\n" +
                                         "/close    - العودة للقائمة الرئيسية\n";

        public const string Usage = "قم بالاختيار بين:\n" +
                                    "/addrent   - إضافة إيجار جديد";

        public const string CarNumberAlreadyExist =
            "رقم السيارة مُستخدم بالفعل، من فضلك أدخل رقم جديد أو أدخل /cancel للرجوع للقائمة الرئيسية";

        public const string CarAdded = "تم تسجيل المركبة بنجاح";
        public const string CarDeleted = "تمت إزالة المركبة بنجاح";


        public static class CarDetails
        {
            public const string Number = "رقم السيارة";
            public const string Brand = "المُصنع";
            public const string Contract = "العقد";
            public const string Model = "موديل";
            public const string Color = "اللون";
            public const string EnterNumber = "إضغط على رقم المركبة";
        }

        public static class Errors
        {
            public const string CarIsUsed = "هذه العربة لم ينتهي إيجارها الحالي بعد";
        }

        public static class Rent
        {
            public const string EnterDate = "أدخل تاريخ بدء الإيجار أو إضغط /today للتسجيل بتاريخ اليوم";
            public const string EnterValidDate = "رجاء إدخال قيمة عددية صحيحة للتاريخ";
            public const string EnterDays = "أدخل عدد أيام الإيجار";
            public const string EnterValidNumber = "أدخل قيمة عديدة صحيحة لعدد أيام الإيجار";
            public const string EnterValidPrice = "أدخل قيمة عددية صحيحة للسعر";
            public const string NotValidStartDay = "هذه المركبة محجوزة في هذا التاريخ";
            public const string EnterPrice = "أدخل السعر";
            public const string SendContractPicture = "أدخل صورة العقد";
            public const string Added = "تم تسجيل الإيجار بنجاح";
        }
    }
}