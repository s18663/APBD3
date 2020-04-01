create procedure proc
    @Studies varchar(100),
    @Semester int
as
begin
    declare @idstudy int;
    declare @idenrollmentcount int;
    declare @idenrollmentCurrent int;
    declare @idenrollmentFuture int;
    select @idstudy = idstudy from Studies where Name = @Studies;
    
    select @idenrollmentCurrent = idenrollment from Enrollment where Semester = @Semester and IdStudy = @idstudy

    select @idenrollmentcount = count(idenrollment) from Enrollment where Semester = @Semester+1 and IdStudy = @idstudy
    if @idenrollmentcount = 0 
        begin
            insert into Enrollment values ( (select max(idenrollment)+1 from Enrollment), @Semester+1, @idstudy, Current_TimeStamp)
        end;
    
    select @idenrollmentFuture =  idenrollment from Enrollment where Semester = @Semester+1 and IdStudy = @idstudy

    update Student set idEnrollment = @idenrollmentFuture where idEnrollment = @idenrollmentCurrent;

end