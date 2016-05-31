//////////////////////////////////////////////////////////////////////////////
GXSCC Beta 236E Instruction
 Copyright (C) 2000-2001,2002 GASHISOFT/GASHISOFT JAPAN
                                                         (translation: Hally)
//////////////////////////////////////////////////////////////////////////////
Public Release Version Update History:

Nov.10, 2002	Beta236E released (English ver.)
Oct.31, 2002	Beta223  released
Jan.29, 2002	Beta127E released (English ver.)
Jan.19, 2002	Beta127  released
Sep.20, 2001	Beta93   released
May 19, 2001	Beta75   released
Aug.07, 2000	Beta38c  released
Aug.03, 2000	Beta38b  released
Jul.28, 2000	Beta38a  released
May.20, 2000	Beta5    released

//////////////////////////////////////////////////////////////////////////////
Requirements:

- Windows95/98/NT4(after SP3)/Me/2000/XP.
- Sound card with 16 bit stereo PCM, PCI connection product is recommended.
- Pentium3 over 600Mhz is recommended for realtime play.
- 128MB memories are recommended. More is better.

* This product is for free, but is still in beta stage.
  So it's focused on operation test than usefulness.
  Please report the most possible bugs which happened at your environment.

//////////////////////////////////////////////////////////////////////////////
Exclusion of Sumpayment:

- We've tested this program at any environments as far as we can. However we
  don't take any responsibilities if it outputs a tremendous roar and as a 
  result your audio or your ears are crashed.
- As same as the above, the entire risk as to the quality and performance of
  the software is borne by you.
- At the end of this text we attached Covenants for Use. It's the same
  contents you've already agreed with downloading.

//////////////////////////////////////////////////////////////////////////////
Usage:

- Extract the archive, though you've already done so.

- Read Readme*.txt, though there're many people who never read ;) Those who
  read this are great, hubba-hubba :)

- Set up the program, and drop standard MIDI files. (*.midÅA*.smf etc.)
  After buffering it will start playing. It can also play file name specified
  at command line option.

- To play RCP format you should install RCPCV produced by Mr.Fumy. Copy
  RCPCV.dll into the same folder as GXSCC.
  RCPCV isn't included in this archive. Please get it by yourself.
  Playing RCP creates "ConvertedRCP.mid". It isn't deleted automatically.
  Please delete by yourself if you don't like it. And this functions as a RCP
  to MID converter :)
  GASHISOFT is never concerned about the result of the convert. And please
  don't attach read only attribute etc. to ConvertedRCP.mid.

  RCPCV is available from here:
  http://www.vector.co.jp/vpack/browse/person/an010012.html

- It doesn't support files with Mac binary. Do something by yourself about it.

- Resource store formats (e.g. *.rmi) aren't supported.

- Basically, instruments are in GM arrangements. Files for old MIDI modules
  are quite messed up, or don't sound at all.
  Currently it supports capital instruments only. Any values specified at CC00
  bring nothing except capital instruments.

= Hidden Mode

  Do something to GXSCC logo, it enters mysterious "GXOPLL?" mode.

  - It's just an "addition."
  - In any case it's also arranged for GM map properly. GASHISOFT original
    distortion guitar is set as the only one user instrument.
  - Currently it supports just 16 parts.
  - Further improvement for this mode isn't planned.
  - Potentially it might be disappeared in the future.
  - Don't appreciate this more than GXSCC even if you make a mistake. The
    author will cry :) 

= Sound Module Specification

  - Standard (SCC mode)
    16 channels x 2 ports. Max 46 voices.
    You can regard it as SCC x 8 + AY-3-8910 compatible x 2.
    Currently snare drum is an only instrument which uses two voices.

  - Hidden Mode (OPLL!? mode)
    16 channels. Max 41 voices.
    You can regard it as OPLL 9 voices mode x 4 + rhythm mode x 1.
    Up to 36 phrases are available. Other 5 voices are only for rhythms.
    Same as OPLL specification, each one voice is usable for bass/snare/tom/
    hihat/cymbal.
    e.g. If cymbal is played during cymbal playing, former one is disappeared
    and newer one starts.

  See details in attached implementation text (for SCC only).



//////////////////////////////////////////////////////////////////////////////
User Interface Control:

Play		Start playing.
                Song loops infinitely unless you stop playing.
                Song is restarted from the top in case you press it during
                playing.
Fast		Skip (*). Push twice to faster, Push thrice to restore.
Stop		Stop playing.
Pause		Pause playing. Push again to restart from that point.
Authoring	Output the current song to WAVE file.
		Don't execute anything until it finishes.
		End the program if you want to stop by all means. 
Config		Set parameters.


Mute		Mute channel (*).
Position	Set play position (*). It's valid only during playing.
                It's too buggy in this version yet.

(*) More buffers delay reactions.


//////////////////////////////////////////////////////////////////////////////
Config (Preference):

- Output Device
  Select sound device to play.
  Microsoft Sound Mapper is set as default.
  (Depending on OS and versions, it might be displayed as other names such as
  "Wave Mapper," but anyway something featuring "Microsoft" on the top of the
  name means this.)

- Output Frequency
  Specify stream frequency. It's applied for both playing and authoring.
  Don't specify any frequencies your sound device don't support.
  (Comparatively newer OS automatically converts frequencies which is not
  supported. It often causes the low quality of sound or the waste of CPU
  load.)

- Auto Detune
  Specify frequency shift width for each channel.
  "None" often causes sound mistakes. So usually no use is recommended.

- Redraw Timing
  Specify the interval to draw main panel. Increasing frames require high CPU
  load, so playing may become intermittent.

- Instrument Set
  Specify tone set. "SCC like Full-Set" and "Famicom like Set" are tone
  maps at least adjusted to GM format.
  Basically "All - Set" except them are for maniacs.

- Drumset
  Specify drum sets. Current version has "GASHISOFT PSG-Drum" only.
  It supports the most basic instruments in GM format.
  And some instruments such as PC49 (Orchestra) or PC51 (KICK&SNARE) used in
  SC-88Pro are reproduced by similar sound depending on the author's whim.

- Monaural Output
  Check to output monaurally. 
  * Authoring with this mode outputs stereo file with same left/right
    waveforms.

= Buffer Setting

  - Buffer Brock Size
    Specify size for stream buffer. Machines with low speed CPU need many
    buffers. Large buffer size often brings long blank at start of playing, or 
    delay at reflection from UI control. On the contrary, small buffer size
    causes intermittent sound.

  - Buffer Count
    Specify the number of stream buffers. This counts total number of
    buffers including three internal buffers for using triple buffering, so
    buffers displayed on play monitor subtract three from specified buffers.
    * total buffer length = BufferBlockSize * ( BufferCount + 3 )

  - Don't stop at Buffer underrun
    Check to continue playing even if buffer underrun happens.
    But of course sound becomes intermittent.

= Equalizing

  - Use Low-pass Filter
    Check to use low-pass filter. Specify cut-off frequency at next step.

  - (Lowpass)Cut-off Frequency
    Specify cut-off frequency for low-pass filter. Valid range is from 100 to
    the half value specified at Output Frequency. 

  - About
    Show the version information.
    Privilege 1: You can access GASHISOFT regular homepage.
    Privilege 2: You can email to GASHISOFT.

//////////////////////////////////////////////////////////////////////////////
Main changes from previous public release (Beta 127E) :

- Supported files for two ports and up to 32 tracks.

- Drastically changed display, changes are too many to be mentioned here.
  (Added CC00 display though it's not related to playing yet.)

- Increased volume to the degree not distorted, since many people complained
  that was too small.

- Now you can specify playing file at command line. Dropped icon starts the
  program and playing. 

- Added file association function. Playlist isn't implemented yet, so please
  make do with it. (You can use Exproler etc. as an alternate of playlist.)

- Added checker for reduplicated start, and proper measures for it. (It passes
  playing file to former GXSCC if it has already started. Later GXSCC finishes
  silently.)

- Now you can send e-mail to GASHISOFT via About dialog. (Simply it starts
  your ordinary mailer software.)

- Now song pauses while using Config dialog.

- Simpler messages are displayed not to be bothered by [OK] button in case
  frequently dialogs bring irksome situations.
  Slightly larger Kaori-chan appears <- important =)

- Mysterious black-out behavior.

- (This text added by GASHI on Nov.10) It was made to perform processing of
  system-menu, caption-bar, minimize-button, and a close-button. on its own
  account.

- (This text added by GASHI on Nov.10) When the remainder of a buffer became
  40% or less, the priority of the thread for renderings was raised
  temporarily. At this time, drawing processing becomes deferment.

- (This text added by GASHI on Nov.10) The pre-process (acquisition of a
  music name, play time, etc.) of buffering was accelerated considerably.
  When strangely long music (it is damaged when the most.) is made to read,
  it is a measure for lapsing into the state near freezing.

- (This text added by GASHI on Nov.10)
  Since processing may be taken if processing of caption-bar is left to
  Windows, it decided to process by GXSCC.

- (This text added by GASHI on Nov.10) Max-voices increased. (from 36 to 46)

- (This text added by GASHI on Nov.10)
  The volition to creation of the author has increased. ;)

= Fixed Bugs

- Fixed really rude bug that didn't release sound device. Somebody seemed not
  to be able to sound until OS is restarted.
  Most of problems related to sound device seem to be resolved by this fix.

- Computers at low specification sometimes couldn't stop thread for playing or
  authoring, and they might have deadlocked - fixed. 

- End of the program under minimized GXSCC caused disappeared window at next
  startup - fixed.

- Fixed forced finish or abnormal behavior which happened when illegal
  exclusive message was received. Now illegal exclusive message is ignored.

- File name wasn't displayed when 0 byte character was included - fixed.

- Fixed click noise which could sound at joint between buffers during using
  low-pass filter. 

- Fixed click noise which could have sounded at the top of the songfile
  applying authoring.

- When files which GXSCC didn't support were read after proper file was
  played, it misunderstood, and click noise or silent playing were started.
  - fixed.

- Eliminated function that stored entire file to memory for checking whether it
  was supported file. It should fix the long waiting time for acquiring memory
  when you played a huge file by error.

- Fixed bug that title display was messed up when files not allowed to play
  were loaded.

- During authoring or when authoring was finished, it might have been stopped
  with "Buffer underrun" display which could never happen at authoring - fixed.

- Buffer meter was displayed though nothing was played - fixed.

- Fixed bugs in documents.

- (This text added by GASHI on Nov.10) A tone was not freed when specific
  conditions were fulfilled using some tones. The number of simultaneous
  pronunciation became fewer. (Sound may stop having come out finally) - fixed.


  Bugs above were fixed. (hopefully). If you find other fixes, perhaps
  they're unnoticed, or forgotten, or not reported etc. 


//////////////////////////////////////////////////////////////////////////////
Known Bugs and Problems:

= Bugs:

  - White noise frequency at 48KHz playing frequency may be different.

  - It hardly checks propriety of data currently playing, so could be crashed
    by broken data.

  - It's about time to fix play position detecter.

  - As same as above, play position doesn't catch up with fast-forward at all.

  - Second GXSCC started without options causes annoyance to GXSCC already
    working.

  - It starts authoring even if data cannot be played.

  - (This text added by GASHI on Nov.10) Authoring will be interrupted if
    more-starting-GXSCC is carried out during authoring.

  - (This text added by GASHI on Nov.10) While opening the Config dialog 
    during reproduction, it will be set to pause if more-starting-GXSCC is 
   carried out.

  - (This text added by GASHI on Nov.10) Although the intention corresponding
    to shortcut-to-a-file, it is impossible unawares.

= Problems:

  - UI featuring fake buttons etc. isn't stylish at all, it looks like
    amateurish.

  - Doesn't it allow to adjust values for pan and pitch bend? or even for
    volume!!

  - Instruments sets are poor. Instruments except some types which the author
    often uses aren't considered.

  - Isn't window too huge? Although it seems bigger than ever...

  - XG isn't supported. In some cases it seems not to sound at all.

  - There's no method to interrupt both buffering and authoring.

  -!- But the biggest problem is too bedraggled author ;)


//////////////////////////////////////////////////////////////////////////////
Future Plans:


- Loading playlist function... isn't kicked at all yet, so I decided not to
  write optimistic plans. 

  "Release interval should be shorter!"

  This interval since former update was too long. And moreover I postponed
  this release two times.
  
  I should think over what I did. 

  From now on, I'd like to release new versions favorably.


//////////////////////////////////////////////////////////////////////////////
FAQ:

- It displays "Buffer Underrun" and stops playing.

  -> It seems a bit heavy for your CPU. 
     Decrease RedrawTiming. If it isn't enough, increase BufferSize. If it
     isn't enough, decrease OutputFrequency. If it isn't enough, give up
     playing realtime. Or change the CPU.

  -> Check "Don't stop at Buffer-underrun." It forces to play. But the sound
     becomes intermittent. It annoys you, so I cannot recommend.

- It doesn't start playing though "Buffering Now" was disappeared. 

  -> When buffering is processed with terrible speed, a possibility that data
     has broken is dense. It also regards as a broken file if it has
     MacBinary. Similarly, it doesn't support resource formats (rmi etc.).

  -> Even if it's not broken, some kind of data which is too far from GM
     format, such as for older modules or for special modules might not sound.

- It doesn't stop playing, but be intermittent or noisy.

  -> Increase Buffer Block Size.
     Basically this problem depends on OS.
     Small Buffer Block Size tends to cause intermittent play, especially when
     other heavy applications are running. 

  -> Decrease Redraw Timing
     It might be effective if video capability isn't good. Some video devices
     could be heavy under certain resolution and colors, so screen customizing
     might bring good results.

  -> Update sound driver to the newest one.
     The newest may not mean the best, but it's an effective method.
     And devices which are likely to be unrelated apparently (such as video
     driver and a SCSI driver etc.) could affect to play. It seems to be caused
     by bus load.

- Sometimes process remains though program has been ended.

  -> Sometimes recent OS like Windows 2000 causes it. GASHISOFT regards as a
     bug of API related to WaveOut.
     It never deletes any settings, so finish it on Task Manager or something.

- The sound is light. The low frequency is thin.

  -> Basically they're features of wave memory sound module. It can't sound
     abundant low frequencies.

  -> Use Low-pass Filter.

  -> Adjust via play device or amplifier.

- I cannot find the window though it started.

  -> Beta128 had a bug which could cause such a problem.

  -> The setting file seems to have been broken by certain reasons. End GXSCC,
     and delete "GXSCCPreferences.bin". It wll be fixed.

- I had a headache or a nausea while I was using it.

  -> Suppress high frequencies via play device or amplifier.

  -> Use Low-pass Filter.

  * If the above disposals didn't have an effect, it's also recommended that
    you stop using as soon as possible and consult with a doctor. Please note
    that GASHISOFT follows the Covenants for Use, and doesn't take any
    responsibilities.



//////////////////////////////////////////////////////////////////////////////
Contact:

- For those who want to send opinions, impressions, bug reports, demands and
  love messages to the author!:
      mailto:gashisoft@geocities.co.jp
      (Please write in Japanese(ISO-2022-JP) as much as possible.
       Sorry, we can't understand other languages.)

- For those who want to go to GASHISOFT regular home page:
      http://www.geocities.co.jp/SiliconValley-SanJose/8700/

- For those who cannot understand Japanese language, contact via VORC:
      mailto:hally@vorc.org
      http://www.vorc.org

//////////////////////////////////////////////////////////////////////////////
Covenants for Use:

* GASHISOFT and the provider and the administrator of the server who offer
  informations don't have responsibilities about any kind of inevitable,
  special, unexpected damages or indirect damages against third parties, even
  if they are caused by downloading or by using this program (and all contents
  included in the package you downloaded, like the manual).
  You have to download and use them at your own risk.

* You receive copyright for the data generated by this program.
  GASHISOFT is never concerned to generated data. Using data generated by this
  program for commercial purpose and equivalent cases is your right. And
  GASHISOFT is never concerned to any troubles between owner of the original
  copyright caused by illegal data against Japanese Copyright Act or the
  international treaty, even if it's made or released accidentally or
  intentionally.

* GASHISOFT never demands and never refuses any profits by downloading or by
  using this program.

* This program has already passed virus check, but never insists on the 
  exterminating of virus.

* Working of the program was confirmed at any precondition environments as far
  as possible, however it never guarantees to work regularly.

* Only use by the same person who downloaded it is allowed. The same person is
  allowed to use it on more than two computers.

* This program is protected by the Japan Copyright Act and the international 
  treaty. Suppose that the intellectual property rights of this program are
  not transferred to any customers at all.

* Any uses except the specifyed usage are never allowed.
  So you can not do any fixes, improvements, translations, reverse
  assemblings, reverse compilings and reverse engineerings.

* Any transfers, reprints, re-distributions by media like CD-ROM or on
  networks are prohibited unless GASHISOFT consents them.

* You lose your use right if the distribution from "the place where the 
  you gained this program" is prohibited, or if the distribution becomes
  impossible and deleted from the place (and if its reason is announced at the
  place where you gained it). 
  Moreover, suppose that duty to delete all the downloaded programs arises.

//////////////////////////////////////////////////////////////////////////////
