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
