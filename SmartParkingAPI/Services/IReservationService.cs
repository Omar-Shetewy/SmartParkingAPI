﻿namespace SmartParking.API.Services;

public interface IReservationService 
{ 
    Task<IEnumerable<ReservationRecord>> GetAll();
    Task<ReservationRecord> GetById(int id);
    Task<ReservationRecord> GetByUserId(int userId);
    Task<ReservationRecord> Add(ReservationRecord record);
    ReservationRecord Update(ReservationRecord record);
    ReservationRecord Delete(ReservationRecord record);
}
