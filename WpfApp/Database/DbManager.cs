using System;
using System.Collections.Generic;
using System.Text;
using WpfApp.Model;
using WpfApp;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace WpfApp.Controller
{
    public class DbManager
    {
        public async void SaveRecordAsync(Record record)
        {
            using (var db = new Context())
            {
                if (record.Id == 0)
                    db.Records.Add(new Record { Text = record.Text, Log = new Log { DateTime = DateTime.Now} });
                else
                {
                    var rec = db.Records.Where(x => x.Id == record.Id).FirstOrDefault();
                    rec.Text = record.Text;
                    rec.Log = new Log { DateTime = DateTime.Now };
                }

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Record>> LoadData()
        {
            using (var db = new Context())
            {
                return await db.Records.Include(x => x.Log).ToListAsync();
            }
        }

        public async Task<Record> LoadData(int id)
        {
            if (id == 0) return null;

            using (var db = new Context())
            {
                return await db.Records.Include(x => x.Log).Where(r => r.Id == id).FirstOrDefaultAsync();
            }
        }

        private void SaveExecute(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch
            {

            }
        }
    }
}
