﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iFourmi.DataMining.ClassificationMeasures
{
    public class SenstivitySpecificityMeasure : IClassificationQualityMeasure
    {
        public double CalculateMeasure(ConfusionMatrix[] list)
        {
            double measure = 0.0;
            foreach (ConfusionMatrix matrix in list)
            {
                double sensitivity = (double) matrix.TP / (double)(matrix.TP + matrix.FN);
                double specificity = (double)matrix.TN / (double)(matrix.TN + matrix.FP);

                measure += sensitivity * specificity;
            }
            measure /= (double)list.Length;
            return measure;
            
        }

        public override string ToString()
        {
            return "SensXSpec";
        }
    }
}
