namespace CarDetailingStudio.BLL
{
    public enum ServiceCategories : int
    {
        AdmminDetailing = 1,
        AdminCarWash = 2,
        AdminCarpetCleaning = 3,
        EmployeesDetailing = 4,
        EmployeesCarWash = 5,
        EmployeesCarpetCleaning = 6,
        AdmminTireStorage = 7,
        EmployeesTireStorage = 8,
        AdmminTireFitting = 9,
        EmployeesTireFitting = 10,
    }
    
    public enum TypeService : int
    {
        Detailing = 1,
        CarWash = 2,
        CarpetCleaning = 3,
        TireStorage = 4,
        TireFitting = 5,
        ParkingLotAvtomir = 6,
        ParkingAutoHouse = 7,
        DryCleaningKohler = 8

    }

    public enum Employees : int
    {
        AdminCarWash = 1,
        AdminDetailing = 2,
        Employees = 3
    }

    public enum PositionsOfAdministrators
    {
        Administrator = 1,
        Employees = 2,
    }

    public enum PaymentMethod
    {
        cash = 1,
        nonСash = 2,
    }

    public enum StatusOrder
    {
        InWork = 1,
        Completed = 2,
        AwaitingPayment = 4,
        Storage = 5
    }
}
