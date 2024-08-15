using System;
using System.Collections.Generic;
using System.Text;

namespace SportsHub.Data
{
    public class BaseRepository
    {

        private readonly PostgreSQLConnectionProvider postgreSQLConnectionProvider;

        public BaseRepository(PostgreSQLConnectionProvider postgreSQLConnectionProvider)
        {
            this.postgreSQLConnectionProvider = postgreSQLConnectionProvider;
        }

        public PostgreSQLConnectionProvider PostgreSQLConnectionProvider => postgreSQLConnectionProvider;
    }
}
