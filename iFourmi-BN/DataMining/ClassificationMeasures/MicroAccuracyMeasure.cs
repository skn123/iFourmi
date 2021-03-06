﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iFourmi.DataMining.ClassificationMeasures
{
    public class MicroAccuracyMeasure:IClassificationQualityMeasure
    {
        public double CalculateMeasure(ConfusionMatrix[] list)
        {
            double measure = 0.0;
            foreach (ConfusionMatrix matrix in list)
                measure += (double)(matrix.TP + matrix.TN) / (double)matrix.Total;
            measure /= (double)list.Length;
            return measure;
            
        }

        public override string ToString()
        {
            return "micro - accuracy";
        }
    }
}
