using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBarMemo.Models
{
    class TestRepository : IMemoRepository
    {
        private static TestRepository _testRepository = new();

        public static TestRepository GetInstance()
        {
            return _testRepository;
        }

        private TestRepository()
        {

        }

        private List<MemoData> SampleDatas = new()
        {
            new MemoData
            {
                Id = 1,
                MemoBody = "テストメモ1",
                MemoTime = DateTime.Now
            },
            new MemoData
            {
                Id = 2,
                MemoBody = "テストメモ2",
                MemoTime = DateTime.Now
            },
            new MemoData
            {
                Id = 3,
                MemoBody = "テストメモ3",
                MemoTime = DateTime.Now
            },
            new MemoData
            {
                Id = 4,
                MemoBody = "テストメモ4",
                MemoTime = DateTime.Now
            },
        };

        public void Dispose()
        {
            // 特になし
        }

        public List<MemoData> GetMemos()
        {
            return SampleDatas;
        }

        public void SaveMemo(MemoData memo)
        {
            SampleDatas.Add(memo);
        }
    }
}
