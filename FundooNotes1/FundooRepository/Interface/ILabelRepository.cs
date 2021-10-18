﻿using FundooModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        public Task<string> addLabels(LabelModel labelModel);
        public Task<string> deleteLabels(int UserId, string labelName);
        public Task<string> updateLabels(LabelModel labelModel);
        public List<string> getLabels(int UserId);
    }
}
