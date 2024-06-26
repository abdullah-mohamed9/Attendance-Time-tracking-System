﻿using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Repos
{
    public class TrackRepo : ITrackRepo
    {
        readonly dbContext db;
        public TrackRepo(dbContext db)
        {
            this.db = db;
        }

        public void AddTrack(Track track)
        {
            db.Tracks.Add(track);
            db.SaveChanges();
        }

        public void DeleteTrack(int id)
        {
            var track = db.Tracks.FirstOrDefault(t => t.Id == id);
            db.Tracks.Remove(track);
            db.SaveChanges();
        }
        public List<Instructor> GetAllInstructors()
        {
            return db.Instructors.ToList();
        }

        public List<Track> GetAllTracks()
        {
            //get all tracks for a specific supervisor
           // return db.Tracks.Where(t => t.SupervisorID == id).ToList();
            return db.Tracks.Include(t => t.InstructorNavigation).Include(t => t.ProgramNavigation).ToList();
        }
            
        public Track GetTrackById(int id)
        {
            return db.Tracks.Include(t => t.InstructorNavigation).Include(t => t.ProgramNavigation).FirstOrDefault(t => t.Id == id);
        }

        public void UpdateTrack(Track track)
        {
            db.Tracks.Update(track);
            db.SaveChanges();
        }


        public List<Track> GetAllTracksForSuperVisor(int Superid)
        {
            return db.Tracks.Where(x=>x.SupervisorID== Superid).ToList();
        }

        public List<Track> GetTrackBySupervisorId(int id)
        {
            //get all tracks for a specific supervisor
            return db.Tracks.Where(t => t.SupervisorID == id).ToList();
        }
    }
}
