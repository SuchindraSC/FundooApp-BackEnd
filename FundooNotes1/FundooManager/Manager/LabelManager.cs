﻿using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository repository;
        public LabelManager(ILabelRepository repository)
        {
            this.repository = repository;
        }

        public Task<string> addLabels(LabelModel label)
        {
            try
            {
                return this.repository.addLabels(label);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> deleteLabels(int UserId, string labelName)
        {
            try
            {
                return this.repository.deleteLabels(UserId, labelName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<string> updateLabels(LabelModel labelModel)
        {
            try
            {
                return this.repository.updateLabels(labelModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<string> getLabels(int UserId)
        {
            try
            {
                return this.repository.getLabels(UserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
