﻿    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace OrderingKioskSystem.Domain.Entities.Base
    {
        public abstract class Entity : IDisposable
        {
            protected Entity()
            {
                ID = Guid.NewGuid().ToString("N");
                NgayTao = NgayCapNhatCuoi = DateTime.Now;
            }

            [Key]
            public string ID { get; set; }


            public string? NguoiTaoID { get; set; }
            public DateTime? NgayTao { get; set; }

            public string? NguoiCapNhatID { get; set; }
            public DateTime? NgayCapNhatCuoi { get; set; }

            public string? NguoiXoaID { get; set; }
            public DateTime? NgayXoa { get; set; }


            [NotMapped]
            private bool IsDisposed { get; set; }

            #region Dispose
            public void Dispose()
            {
                Dispose(isDisposing: true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool isDisposing)
            {
                if (!IsDisposed)
                {
                    if (isDisposing)
                    {
                        DisposeUnmanagedResources();
                    }

                    IsDisposed = true;
                }
            }

            protected virtual void DisposeUnmanagedResources()
            {
            }

            ~Entity()
            {
                Dispose(isDisposing: false);
            }
            #endregion Dispose

        }
    }
