﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_SimpleProject
{
    [ServiceContract]
    interface IBetService
    {
        [OperationContract]
        string GetValue(int i);

        [OperationContract]
        List<double> CalculateSin(double[] x);

        [OperationContract]
        List<Person> GetPersons();


    }
}
