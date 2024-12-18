﻿namespace _0_Framework.Application;

public class ApplicationMessage
{
    public const string RecordNotFound = "رکورد با اطلاعات درخواست شده یافت نشد.";
    public const string DuplicatedRecord = "امکان ثبت رکورد تکراری وجود ندارد.";
    public const string InvalidModelState = "مقادیر خواسته شده را بدرستی وارد کنید.";
    public const string MessageSendSuccess = "نظر شما با موفقیت ثبت شد.";
    public const string MessageSendWrong = "نظر شما ثبت نشد مجددا تلاش فرمایید.";
    public const string PasswordNotMatch = "پسورد وارد شده با تکرار آن برابر نمیباشد.";
    public const string WrongUserPass = "نام کاربری یا کلمه رمز اشتباه است.";
    public const string InvalidCountOfProduct = "تعداد کالای درخواستی از موجودی انبار بیشتر است.";
    public const string EnterRefId = "لطفا شماره پیگیری پرداخت را وارد کنید.";
    public const string EnterPostalCode = "لطفا کد رهگیری پستی را وارد کنید.";
    public const string PayIsSuccessFull="پرداخت با موفقیت انجام شد.جهت دیدن وضعیت سفارش به منو فروشگاه من مراجعه کنید.";
    public const string PayIsWrong="پرداخت ناموفق. در صورت کسر مبلغ از حساب شما، مبلغ تا 24 ساعت آینده به حساب شما بازخواهد گشت.";
}