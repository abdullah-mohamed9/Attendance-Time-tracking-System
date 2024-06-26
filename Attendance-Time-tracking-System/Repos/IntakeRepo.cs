﻿using Attendance_Time_tracking_System.Models; // Add this using directive if Program class is defined in this namespace
using Microsoft.EntityFrameworkCore;
namespace Attendance_Time_tracking_System.Repos
{
    public class IntakeRepo : IIntakeRepo
    {
        readonly dbContext db;
        public IntakeRepo(dbContext db)
        {
            this.db = db;
        }
        public List<IntakeViewModel> GetAll()
        {
            var allintakes = db.Intakes
                .Where(i => i.status == true)
                .Join(db.IntakesProgram, i => i.Id, inp => inp.IntakeId, (i, inp) => new { Intake = i, IntakeProgram = inp })
                .Join(db.Programs, pair => pair.IntakeProgram.ProgramId, p => p.Id, (pair, p) => new IntakeViewModel
                {
                    IntakeId = pair.Intake.Id,
                    IntakeName = pair.Intake.Name,
                    ProgramName = p.Name,
                    stdNo = db.Students.Count(std => std.IntakeID == pair.Intake.Id)
                })
                .ToList();
            return allintakes;
        }


        public Intake GetById(int id)
        {
            return db.Intakes.Find(id);
        }
            
        public void add(IntakeViewModel _intake)
        {
            var intakeName = new Intake { Name = _intake.IntakeName };
            db.Intakes.Add(intakeName);
            db.SaveChanges();
            addToIntakeProgram(_intake);

        }
        public void UpdateIntake(Intake _Data)
        {
            var model = db.Intakes.FirstOrDefault(a => a.Id == _Data.Id);
            model.Name = _Data.Name;
            db.SaveChanges();

        }
        public void DeleteIntake(int id)
        {
            Intake _intake = db.Intakes.FirstOrDefault(a => a.Id == id);
            _intake.status = false; 
            db.SaveChanges();

        }
        public void addToIntakeProgram(IntakeViewModel data)
        {
            // 1. intake name
            var intakeData = db.Intakes.FirstOrDefault(a=> a.Name == data.IntakeName);
            var programData = db.Programs.FirstOrDefault(a=> a.Name == data.ProgramName);
            var newdata = new IntakeProgram { IntakeId = intakeData.Id, ProgramId = programData.Id } ;
            db.IntakesProgram.Add(newdata);
            db.SaveChanges();
        }

        public List<Intake> GetAllIntakes()
        {
            var model = db.Intakes.ToList();
            return model;
        }

        public bool GetByName(string _name,string _ProgramName)
        {
            var intake = db.Intakes
                       .Include(i => i.intakePrograms)
                       .ThenInclude(ip => ip.ProgramNavigation)
                       .FirstOrDefault(i => i.Name == _name);
            if (intake != null)
            {
                // If the intake exists, return the program name to which it belongs
                var programName = intake.intakePrograms.FirstOrDefault()?.ProgramNavigation?.Name;
                return programName == _ProgramName ? true : false;
            }

            return false;
        }
    }
}
