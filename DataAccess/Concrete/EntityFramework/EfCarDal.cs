﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapContext>, ICarDal
    {
        public List<CarDetailDto> GetAllCars()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join color in context.Colors
                             on c.ColorId equals color.ColorId

                             
                             select new CarDetailDto
                             {
                                 CarId = c.CarId,
                                 CarName = c.CarName,
                                 BrandId = b.BrandId,
                                 DailyPrice = c.DailyPrice,
                                 ColorId = c.ColorId,
                                 BrandName = b.BrandName,
                                 Description = c.Description,
                                 ColorName = color.ColorName,
                                 ModelYear = c.ModelYear,
                                 MinFindexPoint=c.MinFindexPoint ?? 0,

                                 CarImagesPaths = (from cI in context.CarImages
                                                   where cI.CarId == c.CarId
                                                   select cI.ImagePath).ToList()

                             };
                return result.ToList();
            }
        }

        public CarDetailDto GetCarDetails(int id)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join color in context.Colors
                             on c.ColorId equals color.ColorId
                             
                             where c.CarId==id
                             select new CarDetailDto
                             {
                                 CarId =c.CarId,
                                 CarName=c.CarName,
                                 BrandId=b.BrandId,
                                 DailyPrice=c.DailyPrice,
                                 ColorId=c.ColorId,
                                 BrandName=b.BrandName,
                                 Description=c.Description,
                                 ColorName=color.ColorName,
                                 ModelYear=c.ModelYear,
                                 MinFindexPoint = c.MinFindexPoint ?? 0,
                                 CarImagesPaths =(from cI in context.CarImages
                                                 where cI.CarId == c.CarId
                                                 select cI.ImagePath).ToList()

                             };
                return result.FirstOrDefault();
            }
        }
    }
}
