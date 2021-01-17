using Project_4;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbManager
{
    public class DBManager
    {
        DbContext1 DataBase = new DbContext1();

        //all Users functions - get list, add new User, delete current User, and update current User
        #region UsersFunctions
        public List<Users> GetUsers()
        {
            return DataBase.Users.ToList();
        }



        public void AddUsers(Users Value)
        {
            DataBase.Users.Add(Value);

        }

        public void DeleteUsers(Users Value)
        {
            DataBase.Users.Remove(Value);

        }

        public void UpdateUsers(Users Value)
        {
             DataBase.Users.Update(Value);
        }


        #endregion

        //all CarsType functions - get list, add new CarsType, delete current CarsType, and update current CarsType
        #region CarsTypeFunctions
        public void AddCarsType(CarsType Value)
        {
            DataBase.CarsType.Add(Value);

        }

        public List<CarsType> GetCarsType()
        {
            return DataBase.CarsType.ToList();
        }

        public void DeleteCarsType(CarsType Value)
        {
            DataBase.CarsType.Remove(Value);

        }

        public void UpdateCarsType(CarsType Value)
        {
            DataBase.CarsType.Update(Value);

        }
        #endregion

        //all RentInfos functions - get list, add new RentInfo, delete current RentInfo, and update current RentInfo
        #region RentInfoFunctions
        public void AddRentInfo(RentInfo Value)
        {
            DataBase.RentInfo.Add(Value);

        }

        public List<RentInfo> GetRentInfo()
        {
            return DataBase.RentInfo.ToList();
        }

        public void DeleteRentInfo(RentInfo Value)
        {
            DataBase.RentInfo.Remove(Value);

        }

        public void UpdateRentInfo(RentInfo Value)
        {
            DataBase.RentInfo.Update(Value);

        }
        #endregion

        //all CarsInfo functions - get list, add new CarsInfo, delete current CarsInfo, and update current CarsInfo
        #region CarsInfoFunctions
        public void AddCarsInfo(CarsInfo Value)
        {
            DataBase.CarsInfo.Add(Value);

        }

        public List<CarsInfo> GetCarsInfo()
        {
            return DataBase.CarsInfo.ToList();
        }

        public void DeleteCarsInfo(CarsInfo Value)
        {
            DataBase.CarsInfo.Remove(Value);

        }

        public void UpdateCarsInfo(CarsInfo Value)
        {
            DataBase.CarsInfo.Update(Value);

        }
        #endregion

        //function to save changes on DataBase
        public void SaveChanges()
        {
            DataBase.SaveChanges();

        }

    }
}
