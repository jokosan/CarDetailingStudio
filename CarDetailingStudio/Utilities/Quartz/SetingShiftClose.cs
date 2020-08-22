using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDetailingStudio.Utilities.Quartz
{
    public class SetingShiftClose
    {
        public static async void Start()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<AutomaticShiftClose>().Build();

            ITrigger trigger = TriggerBuilder.Create()    // создаем триггер
                .WithIdentity("trigger1", "group1")       // идентифицируем триггер с именем и группой
                .StartNow()                               // запуск сразу после начала выполнения
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(22, 00))
                .Build();                                 // создаем триггер

            await scheduler.ScheduleJob(job, trigger);    // начинаем выполнение работы
        }
    }
}