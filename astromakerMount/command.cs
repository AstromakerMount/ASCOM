public enum command
{

    AbortSlew = 0,
    AbortSlewDone,

    MoveAxis,
    MoveAxisDone,

    SlewTo,
    SlewToDone,

    Park,
    ParkDone,

    SetPark,
    SetParkDone,

    Sync,
    SyncDone,

    FindHome,
    FindHomeDone,

    setSlewRate,
    setSlewRateDone,

    setGuideRate,
    setGuideRateDone,

    Track,
    TrackDone,

    StopTrack,
    StopTrackDone

}