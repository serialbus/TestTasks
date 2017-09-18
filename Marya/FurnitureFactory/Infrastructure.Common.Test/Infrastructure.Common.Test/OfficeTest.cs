using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Infrastructure.Common.Services;
using Infrastructure.Common.Models;
using System.Linq;

namespace Infrastructure.Common.Test
{
	[TestClass]
	public class OfficeTest
	{
		[TestMethod]
		public void CheckToCreateNewGuidAndSchedule()
		{
			// Action
			var office = new Office { Name = "Новый офис" };

			// Assert
			// Проверяем создаётся уникальный GUID для нового офиса или нет 
			Assert.AreNotEqual(Guid.Empty, office.Uid);
			// Проверяем создаётся ли расписание работы офиса или нет
			Assert.IsNotNull(office.DailySchedule);
		}

		[TestMethod]
		public void CheckToAddingTimePointToDailySchedule()
		{
			// Arrange
			var office = new Office { Name = "Новый офис" };
			var time = new TimeSpan(5, 35, 46);

			// Act
			office.AddDailyMeetingTime(time);

			// Assert
			Assert.AreEqual(1, office.DailySchedule.Count()); // В расписание добавлено время проведения замера и только одно
			Assert.AreEqual(time, office.DailySchedule.First()); // добавленое время проведения замера верно
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void CheckToAddingWrongTimePointToDailySchedule()
		{
			// Arrange
			var office = new Office { Name = "Новый офис" };
			var time = new TimeSpan(36, 0, 0);

			// Act
			office.AddDailyMeetingTime(time);

			// Assert
			// Exception!!!
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void CheckToAddingExistingTimePointToDailySchedule()
		{
			// Arrange
			var office = new Office { Name = "Новый офис" };
			var time = new TimeSpan(14, 30, 0);

			// Act
			office.AddDailyMeetingTime(time);
			office.AddDailyMeetingTime(time);

			// Assert
			// Exception!!!
		}
	}
}
