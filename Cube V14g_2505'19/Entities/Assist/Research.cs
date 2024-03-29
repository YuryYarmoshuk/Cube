﻿using System;
using System.Windows.Forms;

using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Runtime.InteropServices;
using SolidWorks.Interop.cosworks;
using System.Collections.Generic;

namespace Cube_V11
{
    class Research
    {
        public SLDManager application;

        Array bodySides;

        public ICWStudyManager StudyMngr;
        public dynamic study;

        public double webDensity; // плотность сетки
        public double tlCurrent;
        
        //public static readonly double el = 59.03912785300745;
        //public static readonly double tl = 2.9519563926503731;

        public static readonly double el = 59.03912785300745 * 4;
        public static readonly double tl = 2.9519563926503731 * 4;

        public List<object[]> nodeLists;
       
        //public static readonly double el = 0.115310796587905;
        //public static readonly double tl = 0.00576553982939525;


        string studyName = "Strength";

        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="app">Ссылка на стороннее приложение</param>
        public Research(SLDManager app, Array bodySides)
        {
            application = app;
            this.bodySides = bodySides;
            CreateStudy();
            webDensity = el;
            tlCurrent = tl;

            nodeLists = new List<object[]>();

            //study = CreateStudy();
        }

        public void CreateStudy()
        {
            var CWAddinCallBack = application.swApp.GetAddInObject("SldWorks.Simulation");
            var COSMOSWORKS = CWAddinCallBack.COSMOSWORKS;
            var ActDoc = COSMOSWORKS.ActiveDoc();
            StudyMngr = ActDoc.StudyManager();
            study = StudyMngr.GetStudy(StudyMngr.ActiveStudy);
            if (study == null)
            {
                int errorCode;
                study = StudyMngr.CreateNewStudy(studyName, 0 /*cwStaticAnalysis*/, 0 /*cwSolidElementMesh*/, out errorCode /*errCode*/);
            }

        }

        public void CreateMesh() //создать сетку
        {
            CWMesh cwMesh = study.Mesh;
            cwMesh.Quality = 0;
           
            //double el, tl;
            //CWMesh.GetDefaultElementSizeAndTolerance(3, out el, out tl);
            int errorCode = study.CreateMesh(3, webDensity, tlCurrent);

            if (errorCode != 0)
            {
                for (int i = 1; i < 100; i++)
                {
                    //webDensity -= 2; tlCurrent -= 0.2;
                    //webDensity += el; tlCurrent += tl;
                    webDensity /= 2; tlCurrent /= 2;
                    errorCode = study.CreateMesh(3, webDensity, tlCurrent);
                    if (errorCode == 0) { return; }
                }
                // Написать исключение
                throw new Exception();
            }
            nodeLists.Add(cwMesh.GetNodes());
        }

        public void MaterialSet()
        {
            var SolidMgr = study.SolidManager;

            int rev, rev2;
            var SolidComp = SolidMgr.GetComponentAt(0, out rev);
            var SolidBody = SolidComp.GetSolidBodyAt(0, out rev2);
            bool boolstatus = SolidBody.SetLibraryMaterial(@"C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\lang\english\sldmaterials\solidworks materials.sldmat", "ABS PC");//ABS PC

        }

        public void FixFace()
        {
            var swSelObj = application.swModel.ISelectionManager.GetSelectedObject5(1);
            byte[] vPIDarr = (byte[])application.swModel.Extension.GetPersistReference3(swSelObj);
            //var sIDstring = GetStringFromID(swApp, swModel, vPIDarr);
            //var pDisp1 = GetObjectFromString(swApp, swModel, sIDstring);
            int retVal;
            object pointer = application.swModel.Extension.GetObjectByPersistReference3(vPIDarr, out retVal); ;

            int rev;
            var LBCMgr = study.LoadsAndRestraintsManager;
            var CWRes1 = LBCMgr.AddRestraint(0/* cwRestraintType.cwRestraintFixed */, (new object[] { pointer }), pointer, out rev);
        }

        public void CreateLoad(double force) //создать нагрузку на выбранную грань
        {
            double[] data = new double[6];
            double[] distValue = new double[0];
            double[] forceValue = new double[0];

            int errorCode = 0;

            CWStudy r = study;

            object pointer = application.swModel.ISelectionManager.GetSelectedObject6(1, -1);
            var LBCMgr = r.LoadsAndRestraintsManager;

            data[0] = 1; data[1] = 1; data[2] = 1; data[3] = 1;
            data[4] = 1; data[5] = 1;

            object[] forceArray = { data[0], data[1], data[2], data[3], data[4], data[5] };
            var test = new object[] { pointer };
            var CWRes3 = LBCMgr.AddForce3(1, 0, -1, 0, 0, 0, (distValue), (forceValue), false, false, 0, 0,
               0, force, (forceArray), false, false, (test), null, false, out errorCode);
            /*if (errorCode != 0)
            {
                MessageBox.Show(errorCode.ToString());
            }*/
        }

        public void RunAnalysis()//начать исследование
        {
            CWStudy r = study;
            dynamic StaticOptions = r.StaticStudyOptions;
            StaticOptions.SolverType = 2;
            int errorCode = r.RunAnalysis();
        }

        public Double GetStress()
        {
            CWStudy r = study;
            var CWResult = r.Results;
            if (CWResult == null)
            {
                MessageBox.Show("NO RESULTS: STRESS");
            }
            var boolstatus = application.swModel.Extension.SelectByID2("Top Plane", "PLANE", 0, 0, 0, false, 0, null, 0);
            var swSelObj1 = application.swModel.ISelectionManager.GetSelectedObject5(1);
            int errCode;
            var Stress_Nodal = CWResult.GetMinMaxStress(9, 0, 0, null, 1, out errCode);//напржение

            object[] strainResults = (object[])CWResult.GetStrain(0, 1, null, out errCode);

            return Stress_Nodal[3];
        }

        public object[] GetStressObjectMass()
        {
            CWStudy r = study;
            var CWResult = r.Results;
            if (CWResult == null)
            {
                MessageBox.Show("NO RESULTS: STRESS");
            }
            var boolstatus = application.swModel.Extension.SelectByID2("Top Plane", "PLANE", 0, 0, 0, false, 0, null, 0);
            var swSelObj1 = application.swModel.ISelectionManager.GetSelectedObject5(1);
            int errCode;

            object[] stressResults = (object[])CWResult.GetStress(0, 1, null, (int)swsStrengthUnit_e.swsStrengthUnitPascal, out errCode);

            return stressResults;
        }

        public object[] GetStrainObjectMass()
        {
            CWStudy r = study;
            var CWResult = r.Results;
            if (CWResult == null)
            {
                MessageBox.Show("NO RESULTS: STRESS");
            }
            var boolstatus = application.swModel.Extension.SelectByID2("Top Plane", "PLANE", 0, 0, 0, false, 0, null, 0);
            var swSelObj1 = application.swModel.ISelectionManager.GetSelectedObject5(1);
            int errCode;

            object[] strainResults = (object[])CWResult.GetStrain(0, 1, null, out errCode);

            return strainResults;
        }

        public Double GetDisplacement()
        {
            CWStudy r = study;
            var CWResult = r.Results;
            if (CWResult == null)
            {
                MessageBox.Show("NO RESULTS: DISPLACEMENT");
            }
            var boolstatus = application.swModel.Extension.SelectByID2("Top Plane", "PLANE", 0, 0, 0, false, 0, null, 0);
            var swSelObj1 = application.swModel.ISelectionManager.GetSelectedObject5(1);
            int errCode2;

            dynamic Disp_Nodal = CWResult.GetMinMaxDisplacement(3, 0, null, 3, out errCode2);//смещение

            return Disp_Nodal[3];
        }

        public void deleteStudy()
        {
            StudyMngr.DeleteStudy(studyName);
        }


        public void BodyParts_Select(int index)
        {
            var n = bodySides.GetValue(index) as Entity;
            n.Select(false);
        }


    }
}
