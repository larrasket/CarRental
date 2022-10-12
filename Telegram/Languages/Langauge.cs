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
        public const string UnkownError = "حدث خطأ، تواصل مع المسؤول";

        public const string AdminUsage = "قم بالاختيار بين:\n" +
                                         "/add      - إضافة سيارة\n" +
                                         "/remove   - حذف سيارة\n" +
                                         "/list     - عرض السيارات\n" +
                                         "/cancel    - العودة للقائمة الرئيسية\n";

        public const string Usage = "قم بالاختيار بين:\n" +
                                    "/addrent      - إضافة إيجار جديد\n" +
                                    "/cancelrent   - إلغاء إيجار\n" +
                                    "/editrent   - تعديل إيجار\n" +
                                    "/editcontract - تعديل صورة عقد الإيجار\n" +
                                    "/showall      - عرض جميع السيارات\n" +
                                    "/showcarsinrent - عرض السيارات الموجودة في إيجار حاليًا\n" +
                                    "/showcarsnotinrent - عرض السيارات غير الموجودة في إيجار حاليًا\n" +
                                    "/fine - إضافة غرامة\n" +
                                    "/mntnc - إضافة فاتورة صيانة\n" +
                                    "/cmntnc - إضافة فاتورة صيانة دورية\n" +
                                    "/report - الحصول على تقرير\n" +
                                    "/complete - تسجيل استلام ايجار";

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
            public const string Rent = "الإيجار";
        }

        public static class Errors
        {
            public const string CarIsUsed = "هذه العربة لم ينتهي إيجارها الحالي بعد";
        }

        public static class Rent
        {
            public const string StartDay = "تاريخ البداية";
            public const string EndDay = "تاريخ الإنتهاء";
            public const string EnterDate = "أدخل تاريخ بدء الإيجار أو إضغط /today للتسجيل بتاريخ اليوم";
            public const string EnterDays = "أدخل عدد أيام الإيجار";
            public const string NotValidStartDay = "هذه المركبة محجوزة في هذا التاريخ";
            public const string SendContractPicture = "أدخل صورة العقد";
            public const string Added = "تم تسجيل الإيجار بنجاح";
            public const string Cancel = "من فضلك اضغط على رقم العقد";
            public const string Cancelled = "تم إلغاء العقد بنجاح";
            public const string Edited = "تم تعديل العقد بنجاح";
            public const string SendDriver = "أدخل صورة رخصة السائق";

            public const string ContractOptions = "هل تريد إضافة صورة للعقد؟:" +
                                                  "\n" +
                                                  "/yes نعم \n" +
                                                  "/no لا \n";

            public const string EditStartOrEnd = "/start لتعديل تاريخ البداية" +
                                                 "\n" +
                                                 "/end لتعديل تاريخ الانتهاء";
        }

        public static class Fines
        {
        }

        public const string EnterValidDate = "رجاء إدخال قيمة عددية صحيحة للتاريخ";
        public const string EnterValidNumber = "أدخل قيمة عديدة صحيحة";
        public const string EnterValidPrice = "أدخل قيمة عددية صحيحة للسعر";
        public const string EnterPrice = "أدخل السعر";
        public const string EnterNumber = "إضغط على الرقم المراد";
        public const string EnterPicture = "أرسل الصورة";

        public const string NotReceived = "لم يتم استلام المراكب التالية" +
                                          "\n";

        public const string TomorrowReceive = "تستلم غدًا" +
                                              "\n";
    }
}