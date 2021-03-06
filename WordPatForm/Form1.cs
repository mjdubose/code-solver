﻿using System;
using System.Linq;
using System.Windows.Forms;
using PatternDictionary;
using static WordPatForm.HelperExtensions;

namespace WordPatForm
{
    public partial class FrmCipherSolver : Form
    {
        public FrmCipherSolver()
        {
            InitializeComponent();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            txtSolve.Text = "";
           IPatternDictionary pd  = new PatternDictionary.PatternDictionary(@"dictionary.txt");



              //  pd.Edit();
              pd.WriteToTextFile();
              pd = new PatternDictionary.PatternDictionary();

            // var ciphertext = "Q MYQAA HEOS EG OFIFTYPCFO MJPTPIM GPTFO HS QC ZCWZFCBXQHAF GQPIX PC IXFPT YPMMPEC BQC QAIFT IXF BEZTMF EG XPMIETS.";
            //  var ciphertext ="QFXRW KV GMR IFFG FL YEE RZKE VYWV GMR HKVR QYX HKVREW YV MR MNCCERV NXCRI YX FEC FYJ GIRR GYJKXA VMREGRI LIFQ GMR IYKX FL UFZRIGW KX Y QKVVKVVKUUK LEFFC.";

            //var ciphertext = "hjyznot dn ocz kmvxodxz ja rdoccjgydib amjh joczm kzjkgz ocz cdbc jkdidji tjp cvqz ja tjpmnzga";
            //var ciphertext = "sgqdd-ptzqsdqr ne ntq onotkzshnm khud hm nq mdzq bhshdr; sgd nsgdq ptzqsdq hr nm sgd stqmohjd knnjhmf enq sgd dwhs.";
            // THIS ONE CAUSES ISSUES
            // var ciphertext ="f aftz pvztxpp vtz pshfax sc ux pc kyli kchx ftsxhxpsftn qfsi vt yduxvs kxoczm. rccxm zxplivtxo";
            // var ciphertext = "bhvwhugdb lv d fdqfhoohg fkhfn; wrpruurz lv d surplvvrub qrwh; wrgdb lv wkh rqob fdvk brx kdyh. vr vshqg lw zlvhob ndb obrqv";
            // var ciphertext ="qeb qljy lc qrqxkhexjbk, qeb bdvmqfxk hfkd, txp afpzlsboba fk kfkbqbbk qtbkqv-qtl yv xk bkdifpe xozexblildfpq";
            //THIS ONE CAUSES ISSUES
            //  var ciphertext = "ikk dzxp puekp iqz ldduqaik, jea atz puekp uw atz qlftazuep iqz ldduqaik ixs slblxz. puvqiazp";
            // var ciphertext = "fxhfmk wt olxtdfbo lj vtlvxt hnl eykfidtt hyon mla. ontm nfut f dyino ol ontyd lhb dyeypaxlak lvybylbk";
            //THIS ONE CAUSES ISSUES
            // var ciphertext = "rdm qzt gjcgergtm vy g inmjzr ignj zk rdgr lvf jver oevh lvf gnm qnvom, gr xmgkr evr ferzx rdm mej vy rdm uverd.";
            //var  ciphertext = "kgyjmv tnlhyzcs vkjzk olsyczajzkcs flkn js jyijskjac. kncm jhzcjym osgf kfg hckkczv gb knc jhunjxck: .";
            // var ciphertext = "q pucr cuof lrc lmuit: cui cb lwig qt fdupii xccxoi dux lwi clwiy qtul mofttit t. jydul cuzi yigdypix.";
            // var ciphertext = "T NEKZ ZD ZUE NDDOG PEWQMGE T NTGUEO ZD LTXE OELTPEHQZELB, ZD IHDKZ DKLB ZUE EGGEKZTQL IQWZG DI LTIE, QKO GEE TI T WDMLO KDZ LEQHK NUQZ TZ UQO ZD ZEQWU, QKO KDZ, NUEK T WQSE ZD OTE, ZD OTGWDXEH ZUQZ T UQO KDZ LTXEO. - UEKHB OQXTO ZUDHEQM ";
            // var ciphertext = "BN SK HGKS SJPD SK SKXK OCBGI, BD SCZVO GCD LK MPVVKO XKRKPXMJ, SCZVO BD? - PVLKXD KBGRDKBG ";
            //  var ciphertext ="PG XOYHLM XOYLY PZ GH TPUUYLYGRY EYXBYYG XOYHLM WGT JLWRXPRY. PG JLWRXPRY, XOYLY PZ. - MHIP EYLLW ";
            // var ciphertext = "tzebhx cf ohwhb zonetcoa qge rhzb cr njg rhzbhx oj joh njgx tzeh oj joh.";
            //txtCipher.Text = "RGY AFNR TFKFPSHK NYTRXFJ FS D OFFWNRFPY XN RGY EXNBKDU FS NS OFFWN, VXRG DPR OU BYFBKY KXWY VDUJY ODPKFV, VGF XN D RYPPXSXT DPRXNR.";
        

            var codelist = FormatTextForProcessing(txtCipher.Text).Select(x => new Codeword(x, pd)).ToList();
            var tester = new Work(txtCipher.Text.ToLower(), codelist, txtSolve);
            var results = tester.Go();
            var sortedresults = results.Select(textresult => new SortedResults(textresult, CalculateDistanceFromSignature(ReturnEnglishLanguage(), CalculateCharacterFrequencies(textresult)))).OrderBy(o=>o.Frequency).ToList();
            foreach (var x in sortedresults)
            {
                txtSolve.Text = txtSolve.Text + x.Solution + @" " + x.Frequency + Environment.NewLine;
            }
        }
    }
}
