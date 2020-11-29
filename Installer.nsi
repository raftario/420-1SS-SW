!include "MUI2.nsh"

!define NAME "WeatherApp"
!define DA "1837560"
!define FULLNAME "${NAME}.${DA}"
!define REGKEY "Software\${FULLNAME}"

Name "${NAME}"
OutFile "${FULLNAME}.Setup.exe"
InstallDir "$PROFILE\${DA}"
InstallDirRegKey HKCU "${REGKEY}" ""
RequestExecutionLevel user

!define MUI_ICON "Icon.ico"
!define MUI_ABORTWARNING

!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_INSTFILES

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

Section "${NAME} (${DA})"
    SetOutPath $INSTDIR
    File /r "WeatherApp\bin\Release\netcoreapp3.1\*"
    File "Icon.ico"

    WriteRegStr HKCU "${REGKEY}" "" $INSTDIR
    WriteUninstaller "$INSTDIR\${FULLNAME}.Uninstall.exe"

    CreateDirectory "$SMPROGRAMS\${DA}"
    CreateShortCut "$SMPROGRAMS\${DA}\${NAME}.lnk" "$INSTDIR\${NAME}.exe" "" "$INSTDIR\Icon.ico"
    CreateShortCut "$SMPROGRAMS\${DA}\${NAME} - Uninstall.lnk" "$INSTDIR\${FULLNAME}.Uninstall.exe"

    CreateShortCut "$DESKTOP\${NAME} (${DA}).lnk" "$INSTDIR\${NAME}.exe" "" "$INSTDIR\Icon.ico"
SectionEnd

Section "Uninstall"
    Delete "$DESKTOP\${NAME} (${DA}).lnk"
    RMDir /r "$SMPROGRAMS\${DA}"
    RMDir /r $INSTDIR
    DeleteRegKey HKCU "${REGKEY}"
SectionEnd
