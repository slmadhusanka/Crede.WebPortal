using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BIZ.HelperModel
{
    public class LogTable
    {
        private LogTable(string value) { Value = value; }

        public string Value { get; private set; }

        public static LogTable ActivityActions { get { return new LogTable("ActivityActions"); } }
        public static LogTable ActivityFactors { get { return new LogTable("ActivityFactors"); } }
        public static LogTable ActivitySubActions { get { return new LogTable("ActivitySubActions"); } }
        public static LogTable ActivitySubFactors { get { return new LogTable("ActivitySubFactors"); } }
        public static LogTable AllowedDevices { get { return new LogTable("AllowedDevices"); } }
        public static LogTable Audit { get { return new LogTable("Audit"); } }
        public static LogTable AuditTypes { get { return new LogTable("AuditTypes"); } }
        public static LogTable Comments { get { return new LogTable("Comments"); } }
        public static LogTable DimDate { get { return new LogTable("DimDate"); } }
        public static LogTable DimFacility { get { return new LogTable("DimFacility"); } }
        public static LogTable DimGuideline { get { return new LogTable("DimGuideline"); } }
        public static LogTable DimHCW { get { return new LogTable("DimHCW"); } }
        public static LogTable DimMoment { get { return new LogTable("DimMoment"); } }
        public static LogTable DimNote { get { return new LogTable("DimNote"); } }
        public static LogTable DimPPE { get { return new LogTable("DimPPE"); } }
        public static LogTable DimRegion { get { return new LogTable("DimRegion"); } }
        public static LogTable DimSubGuideline { get { return new LogTable("DimSubGuideline"); } }
        public static LogTable DimSubPPE { get { return new LogTable("DimSubPPE"); } }
        public static LogTable DimUnit { get { return new LogTable("DimUnit"); } }
        public static LogTable DistributionListMembers { get { return new LogTable("DistributionListMembers"); } }
        public static LogTable DistributionLists { get { return new LogTable("DistributionLists"); } }
        public static LogTable FacilityType { get { return new LogTable("FacilityType"); } }
        public static LogTable HCWCategory { get { return new LogTable("HCWCategory"); } }
        public static LogTable KeyDates { get { return new LogTable("KeyDates"); } }
        public static LogTable MFA_For_Roles { get { return new LogTable("MFA_For_Roles"); } }
        public static LogTable ModuleDetails { get { return new LogTable("ModuleDetails"); } }
        public static LogTable PermissionDetails { get { return new LogTable("PermissionDetails"); } }
        public static LogTable Program { get { return new LogTable("Program"); } }
        public static LogTable ProgramType { get { return new LogTable("ProgramType"); } }
        public static LogTable ReportsAndRoles { get { return new LogTable("ReportsAndRoles"); } }
        public static LogTable Role { get { return new LogTable("Role"); } }
        public static LogTable SendMails { get { return new LogTable("SendMails"); } }
        public static LogTable ServerDomain { get { return new LogTable("ServerDomain"); } }
        public static LogTable SystemConfiguration { get { return new LogTable("SystemConfiguration"); } }
        public static LogTable SystemSetting { get { return new LogTable("SystemSetting"); } }
        public static LogTable TempAuditHeader { get { return new LogTable("TempAuditHeader"); } }
        public static LogTable TempHCP { get { return new LogTable("TempHCP"); } }
        public static LogTable TempObs { get { return new LogTable("TempObs"); } }
        public static LogTable tmp_hcp { get { return new LogTable("tmp_hcp"); } }
        public static LogTable tmp_obs { get { return new LogTable("tmp_obs"); } }
        public static LogTable UnitType1 { get { return new LogTable("UnitType1"); } }
        public static LogTable SuggestedPriorityType { get { return new LogTable("SuggestedPriorityType"); } }

        public static LogTable PossibleConsequence { get { return new LogTable("PossibleConsequence"); } }
        public static LogTable UnitType2 { get { return new LogTable("UnitType2"); } }
        public static LogTable Users { get { return new LogTable("Users"); } }
        public static LogTable DimResult { get { return new LogTable("DimResult"); } }
        public static LogTable TempActivityFollowContent { get { return new LogTable("TempActivityFollowContent"); } }
        public static LogTable TempActivityFollowAction { get { return new LogTable("TempActivityFollowAction"); } }
        public static LogTable TempActivityFollowSubAction { get { return new LogTable("TempActivityFollowSubAction"); } }
        public static LogTable TempActivityFollowFactor { get { return new LogTable("TempActivityFollowFactor"); } }
        public static LogTable TempActivityFollowSubFactor { get { return new LogTable("TempActivityFollowSubFactor"); } }
        public static LogTable TempActivityFollowHCP { get { return new LogTable("TempActivityFollowHCP"); } }
        public static LogTable ActivityFollowHCP { get { return new LogTable("ActivityFollowHCP"); } }
        public static LogTable AuditHcpActions { get { return new LogTable("AuditHcpActions"); } }
        public static LogTable AuditHcpSubAcions { get { return new LogTable("AuditHcpSubAcions"); } }
        public static LogTable AuditHcpFactors { get { return new LogTable("AuditHcpFactors"); } }
        public static LogTable AuditHcpSubFactors { get { return new LogTable("AuditHcpSubFactors"); } }

        public static LogTable dimErrorCat  { get { return new LogTable("dimErrorCat"); } }
    }
}
